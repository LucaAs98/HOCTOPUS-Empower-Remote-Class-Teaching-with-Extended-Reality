using IKVM.Reflection.Emit;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86;
using static UnityEditor.PlayerSettings;

public class CreateLabels : MonoBehaviour
{
    [SerializeField] Transform center;
    [SerializeField] GameObject containerTooltips;

    private List<Transform> objlabelleft;
    private List<Transform> objlabelright;
    private bool right = true;
    [SerializeField] public bool recursive = false;

    [SerializeField] private Transform label;
    private float distance = 0.5f;
    private float step;
    private float initY;


    void Start()
    {
        Labelling();
    }

    public void Labelling()
    {
        List<Transform> obj = GetChildrens(this.transform);
        obj.Sort(YPositionComparison);
        int nlabel = (int)System.Math.Ceiling(obj.Count / 2.0f);

        step = 1.3f / nlabel;
        float yCenter = center.transform.position.y;
        initY = yCenter - (((int)System.Math.Ceiling(nlabel / 2.0f)) * step);

        objlabelleft = new List<Transform>();
        objlabelright = new List<Transform>();

        foreach (Transform child in obj)
        {
            CreateLabel(child);
        }
    }

    private void CreateLabel(Transform child)
    {
        Vector3 rl;

        if (right)
            rl = this.transform.right;
        else
            rl = -(this.transform.right);

        Vector3 pos = child.transform.position + rl * distance;
        Quaternion rot = label.transform.rotation;

        pos = CheckPosition(pos);

        Transform spawnedModel = Instantiate(label, pos, rot, containerTooltips.transform);

        ToolTip labeltext = spawnedModel.GetComponent<ToolTip>();
        labeltext.ToolTipText = child.name;

        Transform anchor = spawnedModel.GetChild(0);
        anchor.position = child.position;

        //CheckPosition(spawnedModel);
        if (right)
            objlabelright.Add(spawnedModel);
        else
            objlabelleft.Add(spawnedModel);

        right = !right;
    }

    private Vector3 CheckPosition(Vector3 poslabel)
    {
        List<Transform> objlabel;

        if (right)
            objlabel = objlabelright;
        else
            objlabel = objlabelleft;


        if (objlabel.Count > 0)
        {
            List<float> yposition = new List<float>();

            foreach (Transform i in objlabel)
            {
                yposition.Add(i.position.y);
            }

            poslabel.y = yposition.Max() + step;
        }
        else
        {
            poslabel.y = initY;
        }

        return poslabel;
    }

    private float SearchArray(float inValToSearch_, List<float> inArr_)
    {
        if (inArr_ == null || inArr_.Count == 0)
            return 0;
        for (int i = 0; i < inArr_.Count - 1; i++)
        {
            if (inArr_[i] < inValToSearch_ && inArr_[i + 1] > inValToSearch_)
            {
                return inValToSearch_ - inArr_[i] < inArr_[i + 1] - inValToSearch_ ? inArr_[i] : inArr_[i + 1];
            }
        }

        return inArr_.OrderBy(item => System.Math.Abs(inValToSearch_ - item)).First();
    }

    private List<Transform> GetChildrens(Transform parent)
    {
        List<Transform> aux = new List<Transform>();

        foreach (Transform child in parent)
        {
            if (!child.name.Equals("Tooltips"))
            {
                aux.Add(child);
                if (recursive && child.childCount > 0)
                {
                    List<Transform> grandchilds = GetChildrens(child);
                    aux.AddRange(grandchilds);
                }
            }
        }

        return aux;
    }

    private int YPositionComparison(Transform a, Transform b)
    {
        //null check, I consider nulls to be less than non-null
        if (a == null) return (b == null) ? 0 : -1;
        if (b == null) return 1;

        var ya = a.transform.position.y;
        var yb = b.transform.position.y;
        return ya.CompareTo(yb); //here I use the default comparison of floats
    }
}
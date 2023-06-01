
# [**HOCTOPUS: Empower Remote Class Teaching with Extended Reality**]()<br/>


[Lorenzo Stacchio](https://www.unibo.it/sitoweb/lorenzo.stacchio2)\*,
[Andrea Cirina](linkedin.com/in/andrea-cirina-87a435279)\*,
[Luca Asunis](https://www.linkedin.com/in/luca-asunis?miniProfileUrn=urn%3Ali%3Afs_miniProfile%3AACoAAC7swksBCCX_diC1XLaQWmcMON-KsQR9meU&lipi=urn%3Ali%3Apage%3Ad_flagship3_search_srp_all%3BoBHg60UxQfGCTxTSvzequg%3D%3D)\*,
[Gustavo Marfia](https://github.com/qp-qp)<br/>

| [GoodIT '23 Oral]([https://icec23.cs.unibo.it/](http://goodit.campusfc.unibo.it/)) | [paper](xxx) |

<p align="center">
  <img src="images/hoctopus_teaser.png" width="450">
</p>

``` 

HOCTOPUS provides an easy tool for both teachers and students to visualize, manipulate and share 3D objects in a live-stream fashion exploiting peer-to-peer network connections. In particular, HOCTOPUS consists of two main components thought for the two main actors of our use case: teachers and students. For teachers, we provided an MR streaming experience that let the user picks the 3D model for the class she/he wants to teach and straightforward interactions to manipulate during the explanation. For students instead, we developed an AR experience to visualize the streamed 3D object in a non-situated fashion, which real-time twins all the manipulations made by the teacher in the MR experience. Students have also the possibility to interact with the object and ask questions.

In particular, we developed the MR side of the system for the Hololens 2 and the AR one for Android mobile devices.
In practice, HOCTOPUS aims at supporting remote MR class teaching by streaming the teacher's manipulations of 3D models to all the students that joined the class, to enhance and improve the learning process of complex structures and processes.
``` 

## Requirements
* Unity 2019.4.xx

## Features 
* 
* 
## Deployment Hololens 2
1. Go to `File -> Build Settings`. Here change the **platform** by selecting `Universal Windows Platform` and pressing on `Switch platform`. Once you have changed the platform, make sure you have these settings:
<p align="center">
  <img src="images/DeployHololens/FirstSettings.png" width="450">
</p>

2.  Next go to `Player Settings`, make sure you are on the correct platform and scroll down until you find `Publishing Settings`. Here you need to **create a new certificate** and **select it** for this project. At the end of the operation you will have a screen similar to this one:
<p align="center">
  <img src="images/DeployHololens/Certificate.png" width="450">
</p>

3.  Now go to `Mixed Reality -> Toolkit -> Utilities -> Build Window`. 
<p align="center">
  <img src="images/DeployHololens/BuildWindowBuildSection.png" width="450">
</p>

4. In the window that will open, make sure you have the following settings:
<p align="center">
  <img src="images/DeployHololens/BuildWindowBuildSection.png" width="450">
</p>

⚠️**Warning**⚠️. Make sure the folder you are doing the build in is **empty**!
Then click on `Build Unity Project`.

5. Once the build is finished, go to the folder that was created automatically (`Builds --> WSAPlayer`) and open the `.sln` file with visual studio. In the window on the right called `Solution Explorer`,  open the file `Package.appxmanifest` directly from **visual studio**. Click on `Create Package`, and you will see a screen similar to this one:
<p align="center">
  <img src="images/DeployHololens/CreatePackage.png" width="450">
</p>
Here click on `Choose certificate...` and select the one you created **earlier**.

6. Now go back to `Solution Explorer` and right-click on `Package.appxmanifest`, then `Open with... -> XML Editor`.  Here you need to make sure that `PhoneProductId` and `PhonePublisherId` have the **same code**. Also go all the way down and make sure the `Capabilities` are configured like this:
<p align="center">
  <img src="images/DeployHololens/Capabilities.png" width="450">
</p>
🔴**Important**❗🔴 Close Visual Studio, a warning will appear and you will need to **save**. ⚠️ **Do not skip this step.** ⚠️

7. Now **go back to Unity** and in the `Build Window` section click on `Appx Build Options`. Put in the following settings:
<p align="center">
  <img src="images/DeployHololens/Appx.png" width="450">
</p>
Finally click on `Build Appx`.

8. Now you'll just go to `<<NameOfTheProject>>\Builds\WSAPlayer\AppPackages\ <<NameOfTheProject>>_1.0.1.0_ARM64_Test `.
Here you will find the package (`.appx`) to install on the Hololens 2. We recommend that you use the **device portal** for installation.
<p align="center">
  <img src="images/DeployHololens/ExplorerAppx.png" width="450">
</p>



## Demo

The demo is in the ...


## References 
* **Floor Finder** - https://github.com/LocalJoost/FloorFinder.git
* **Outline** - https://github.com/chrisnolet/QuickOutline.git



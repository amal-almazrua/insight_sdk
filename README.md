# Emotiv Insight SDK - Community Edition

Here is the repository to download the latest Emotiv Insight SDK Community Edition, along with code examples and other development tools.

If you have questions or have knowledge to share, please visit our [forum](https://emotiv.com/forum/) which is the hub for our developer community.

## Table of Contents
1. [Latest Release](#latest-release)
2. [Supported Platforms](#supported-platforms)
3. [Connecting Your Insight](#connecting-your-insight)
4. [API Documentation](#api-documentation)
5. [Known Issues](#known-issues)
6. [Apps and Tools](#apps-and-tools)
7. [Support](#support)

## Latest Release
We are currently preparing a stable SDK for release and updating the repository with more examples and instructions. Please check out the [insight-beta-release branch](https://github.com/Emotiv/insight_sdk/tree/Insight-beta-release) for latest changes.

At the moment we have:
* Insight SDK for Win32, Mac, Android
* EmoComposer for Win32, Mac

Coming soon:
* Insight SDK for iOS
* Insight SDK for Ubuntu
* Insight SDK for Win64

## Supported Platforms
* Insight with Bluetooth SMART (Bluetooth 4.0 or Bluetooth Low Energy) connection currently works with the SDK under:
  * Windows 8 or above
  * Mac OS X 10.10 or above (check if Bluetooth LMP Version is 0x6 from System Report)
  * Android 4.4 or above

* Insight with Emotiv Universal USB Receiver currently works with the SDK under:
  * Windows 7 or above
  * Mac OS X 10.8
  * Android 4.3 or above

## Connecting Your Insight
* Windows: Turn on Bluetooth on both Insight and PC, then pair your Insight with Windows built-in Bluetooth service first
* Mac and Android: Turn on Bluetooth on both Insight and Mac/Android device, then start Emotiv app to use (without first pairing)

## API Documentation
The API reference can be found here:

http://emotiv.com/api/insight/3.1.16/

**NOTE:** APIs starting with **IEE_Data**, **IEE_Edf** and **IEE_EngineLocalConnect**, are not available in Insight SDK Community Edition. Those functions are only available in **Insight SDK Premium Edition** and can be downloaded from emotiv.com after purchasing Insight with EEG access.

## Known Issues
* IS_PerformanceMetricGetStressScore, IS_PerformanceMetricGetStressModelParams may return zero on Windows and Mac.

## Apps and Tools

#### Insight Control Panel
If you are looking for Emotiv Control Panel with EPOC before, we have now made a browser version:

https://cpanel.emotivinsight.com/BTLE/

Compatible with Insight via BTLE or [Emotiv USB Universal Receiver](https://emotiv.com/store/product_9.html) on:
* Windows 7+ with Firefox
* Mac OS X 10.9+ with Safari or Chrome

#### App for Android
Please join the Emotiv Beta community https://plus.google.com/u/0/communities/111719780251099691777 to get access to our beta Android app test program, including Insight Consumer app, BrainVis and MentalCommand.

#### App for iOS
We are working hard on the BTLE data transmission issues with iOS 8.3+ and will release the iOS version once it is out of the way - stay tuned!

## Support

Please check out the topic **Insight** on **Emotiv Help Centre**:

https://emotiv.zendesk.com/hc/en-us/categories/200100495-Insight

Our knowledge base is a good source for further reading:

https://emotiv.zendesk.com/hc/en-us
 
Please also visit our [forum](https://emotiv.com/forum/) for bug reports and feature requests.

Happy coding!

The Emotiv Team

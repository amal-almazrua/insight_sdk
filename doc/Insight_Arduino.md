#Notes:

`emostate_serial.py` and `emoArduino.ino` are the scripts made to allow interfacing between the Emotiv Insight and Arduino Uno.

As of now, the python scripts sends values from the Emotiv Insight API to the Arduino over serial. The Arduino parses those values, assigns them to variables then sends over serial.

###Download and Install [Insight SDK Lite](https://emotiv.com/store/product_262.html)

###Download and Install [Xavier Composer](http://wiki.emotiv.com/tiki-index.php?page=Downloads)

####For Python dev:

  - You need to link to directory with `InsightEDK.DLL` and `edk_utils.dll`
  - You can reference the files, or insert them in directory running the python script (which is what I did).
  `C:\Program Files (x86)\Emotiv Insight SDK Lite v1.1.1.0\Dll`
  - requires pySerial


1. Upload `emoArduino.ino` to Arduino Board

2. Run Xavier Composer if not connected to device

3. Run Python script `emostate_serial.py`

####Python
    - `logEmoState` is the function in the script that packs a tuple with various Emotiv States.
    - `valToArduino` sends the data in the tuple over serial to the Arduino.

####Arduino

- Arduino receives serial and parses out values IN ORDER.

    `Time`, `userID`, `wirelessSigStatus`, `Blink`,

    `leftWink`, `rightWink`, `surprise`, `frown`, `clench`,

    `smile`, `longExcitement`, `shortExcitement`, `boredom`,

    `mentalCommandAction`, `mentalCommandPower`.

- Blinky Lights Demo: `blinkLEDs` is a function in the Arduino code that will blink the built in LED on the Arduino Uno, if the `Blink` API call returns 1.

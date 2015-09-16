// intended for use with emostate_serial.py



byte newServoPos = servoMin;
byte newServoPos1 = servoMin;

const byte numLEDs = 15;
byte ledPin[1] =  {13};
byte emoStatus[numLEDs] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

const byte buffSize = 40;
char inputBuffer[buffSize];
const char startMarker = '<';
const char endMarker = '>';
byte bytesRecvd = 0;
boolean readInProgress = false;
boolean newDataFromPC = false;

char messageFromPC[buffSize] = {0};

unsigned long curMillis;

unsigned long prevReplyToPCmillis = 0;
unsigned long replyToPCinterval = 1000;

//=============

void setup() {
  Serial.begin(9600);

    // flash LEDs so we know we are alive
  for (byte n = 0; n < numLEDs; n++) {
     pinMode(ledPin[n], OUTPUT);
     digitalWrite(ledPin[n], HIGH);
  }
  delay(500); // delay() is OK in setup as it only happens once

  for (byte n = 0; n < numLEDs; n++) {
     digitalWrite(ledPin[n], LOW);
  }
    // tell the PC we are ready
  Serial.println("<Arduino is ready>");
}

//=============

void loop() {
  curMillis = millis();
  getDataFromPC();

}

//=============

void getDataFromPC() {

    // receive data from PC and save it into inputBuffer

  if(Serial.available() > 0) {

    char x = Serial.read();

      // the order of these IF clauses is significant

    if (x == endMarker) {
      readInProgress = false;
      newDataFromPC = true;
      inputBuffer[bytesRecvd] = 0;
      parseData();
    }

    if(readInProgress) {
      inputBuffer[bytesRecvd] = x;
      bytesRecvd ++;
      if (bytesRecvd == buffSize) {
        bytesRecvd = buffSize - 1;
      }
    }

    if (x == startMarker) {
      bytesRecvd = 0;
      readInProgress = true;
    }
  }
}

//=============

void parseData() {

    // split the data into its parts
    // assumes the data will be received as (eg) 0,1,35

  char * strtokIndx; // this is used by strtok() as an index
  Serial.println(inputBuffer);

  strtokIndx = strtok(inputBuffer,","); // get the first part
  emoStatus[0] = atof(strtokIndx); //  convert to an integer

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[1] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[2] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[3] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[4] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[5] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[6] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[7] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[8] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[9] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[10] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[11] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[12] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[13] = atof(strtokIndx);

  strtokIndx = strtok(NULL, ","); // this continues where the previous call left off
  emoStatus[14] = atof(strtokIndx);


}

//=============

void replyToPC() {

  if (newDataFromPC) {
    newDataFromPC = false;
    Serial.print("<Time ");
    Serial.print(emoStatus[0]);
    Serial.print(" UserID ");
    Serial.print(emoStatus[1]);
    Serial.print(" wirelessSigStatus ");
    Serial.print(emoSTatus[2]);
    Serial.print(" Blink ");
    Serial.print(emoStatus[3]);
    Serial.print(" leftWink ");
    Serial.print(emoStatus[4]);
    Serial.print(" rightWink ");
    Serial.print(emoStatus[5]);
    Serial.print(" Surprise ");
    Serial.print(emoStatus[6]);
    Serial.print(" Frown ");
    Serial.print(emoStatus[7]);
    Serial.print(" Clench ");
    Serial.print(emoStatus[8]);
    Serial.print(" Smile ");
    Serial.print(emoStatus[9]);
    Serial.print(" longExcitement ");
    Serial.print(emoStatus[10]);
    Serial.print(" shortExcitement ");
    Serial.print(emoStatus[11]);
    Serial.print(" Boredom ");
    Serial.print(emoStatus[12]);
    Serial.print(" MentalCommand Action ");
    Serial.print(emoStatus[13]);
    Serial.print(" MentalCommand Power ");
    Serial.print(emoStatus[14]);
    Serial.println(">");
  }
}

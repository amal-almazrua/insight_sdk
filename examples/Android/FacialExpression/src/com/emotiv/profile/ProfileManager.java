package com.emotiv.profile;

import java.io.File;

import com.emotiv.insight.IEdk;
import com.emotiv.insight.IEdkErrorCode;

import android.os.Environment;
import android.util.Log;

public class ProfileManager {
   static ProfileManager instance;
   public String userName;
   public static ProfileManager shareInstance(){
	   if (instance == null) {
		    instance = new ProfileManager();
	   }
	return instance;
   }
   
   
   public void saveProfile(int userId){
	   String _profileLocation = Environment.getExternalStorageDirectory().getAbsolutePath() + "/EmoProFile/";
	   File profileFolder = new File(_profileLocation);
	     if (!profileFolder.exists()) {
	      profileFolder.mkdirs();
	     }
	     Log.e("save", "save " + IEdk.IEE_SaveUserProfile(userId, _profileLocation + userName + ".emu") +" " + userName);

   }
   
   public boolean loadProfile(int userId){
	   String _profileLocation = Environment.getExternalStorageDirectory().getAbsolutePath() + "/EmoProFile/";
	   File profileFolder = new File(_profileLocation);
	   if (!profileFolder.exists()) {
	    return false;
	   }
	   if ( IEdk.IEE_LoadUserProfile(userId, _profileLocation + userName + ".emu") == IEdkErrorCode.EDK_OK.ToInt()){
		   return true;
	   }
	   saveProfile(userId);
	   if ( IEdk.IEE_LoadUserProfile(userId, _profileLocation + userName + ".emu") == IEdkErrorCode.EDK_OK.ToInt()){
		   return true;
	   }
	   
	   return false;
	  
   }
}

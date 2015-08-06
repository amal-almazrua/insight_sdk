package com.emotiv.facialexpression;

import com.emotiv.dateget.EngineConnector;
import com.emotiv.profile.ProfileManager;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.TextView;

public class CreateUser extends Activity{
	TextView textView;
	ProfileManager profile;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.create_user);
	
	}
	
	
	public void clickedCreate(View v){
		
		textView = (TextView) this.findViewById(R.id.editText1);
		if (textView.length() == 0) return;
		profile = profile.shareInstance();
		profile.userName =  textView.getText().toString();
		this.finish();
		Intent intent = new Intent(this, ContentActivity.class);
		startActivity(intent);
		
	}
	
}

package com.example.yuvalw11.imageserviceforandroid;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.content.Intent;


public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void startService(View view) {
        Intent intent = new Intent(this, ImageServiceService.class);
        startService(intent);
    }

    public void stopService(View view) {
        Intent intent = new Intent(this, ImageServiceService.class);
        stopService(intent);
    }


}

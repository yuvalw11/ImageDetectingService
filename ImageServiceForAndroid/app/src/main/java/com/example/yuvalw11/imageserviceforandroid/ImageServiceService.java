package com.example.yuvalw11.imageserviceforandroid;

import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.IntentFilter;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.os.Environment;
import android.os.IBinder;
import android.content.Intent;
import android.support.annotation.Nullable;
import android.util.Log;
import android.widget.SeekBar;
import android.widget.Toast;

import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class ImageServiceService extends Service {

    private BroadcastReceiver receiver;
    private int SEND_PICTURE_COMMAND = 8;
    private Client client;

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

   @Override
   public void onDestroy() {
       Toast.makeText(this, "Service Destroyed", Toast.LENGTH_LONG).show();
   }


    @Override
    public void onCreate() {
        super.onCreate();
        final IntentFilter filter = new IntentFilter();
        filter.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        filter.addAction("android.net.wifi.STATE_CHANGE");
        this.receiver = new BroadcastReceiver() {
            @Override
            public void onReceive(Context context, Intent intent) {
                WifiManager wifiManager = (WifiManager)context.getSystemService(context.WIFI_SERVICE);
                NetworkInfo networkInfo = intent.getParcelableExtra(WifiManager.EXTRA_NETWORK_INFO);
                if(networkInfo != null) {
                    if (networkInfo.getType() == ConnectivityManager.TYPE_WIFI) {
                        if(networkInfo.getState() == NetworkInfo.State.CONNECTED) {
                            startTransfer();
                        }
                    }
                }
            }
        };
        this.client = new Client();
        this.client.connectToServer(8001);
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this, "Service started", Toast.LENGTH_LONG).show();
        return START_STICKY;
    }

    public void startTransfer() {
        File dcim = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM);
        if (dcim == null) {
            return;
        }

        File[] pics = dcim.listFiles();
        if (pics == null) {
            return;
        }
        int count = 0;

        for(File pic : pics) {
            sendPhotosToService(pic);
        }

    }


    public boolean sendPhotosToService (File pic) {
        try {
            FileInputStream fis = new FileInputStream(pic);
            Bitmap bm = BitmapFactory.decodeStream(fis);
            ByteArrayOutputStream stream = new ByteArrayOutputStream();
            bm.compress(Bitmap.CompressFormat.PNG, 70, stream);
            byte[] imgBytes = stream.toByteArray();
            this.client.sendBytes(imgBytes, pic.getName().getBytes());
            return true;
        }
        catch (Exception e) {
            Log.e("1", "problem in sendPhotosToService");
            return false;
        }
    }


    /*private byte[] createJsonObjToTransfer(byte[] pic, String name) {
        String[] args = new String[2];
        args[0] = name;
        args[1] = new String(pic);
        String jsonString;
        try {
            jsonString = new JSONObject()
                    .put("commandID", SEND_PICTURE_COMMAND)
                    .put("args", (Object)args)
                    .toString();
        }
        catch (Exception e) {
            jsonString = "";
            Log.e("1", "createJsonObjToTransfer: could not create json");
        }
        return jsonString.getBytes();
    }*/


}

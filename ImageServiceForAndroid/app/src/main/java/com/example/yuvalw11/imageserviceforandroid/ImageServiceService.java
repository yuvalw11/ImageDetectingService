package com.example.yuvalw11.imageserviceforandroid;

import android.app.NotificationManager;
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
import android.support.v4.app.NotificationCompat;
import android.util.Log;
import android.widget.SeekBar;
import android.widget.Toast;

import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.util.List;
import java.lang.String;
import java.io.IOException;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class ImageServiceService extends Service {

    private BroadcastReceiver receiver;
    private int SEND_PICTURE_COMMAND = 8;
    private Client client;
    private int numOfPhotos;
    private int index;
    private List<File> files;
    IntentFilter intentFilter= new IntentFilter();;

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

   @Override
   public void onDestroy() {
       Toast.makeText(this, "Service Destroyed", Toast.LENGTH_LONG).show();
   }


   private synchronized void displayNotofication() {
        final NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
        final int notifyId = 1;
        final NotificationManager nm =(NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        builder.setSmallIcon(R.drawable.ic_launcher_background);
        builder.setContentTitle("transfer");
        builder.setContentText("transfering images...");

        new Thread(new Runnable() {
            @Override
            public void run() {
                while(index != numOfPhotos) {
                    builder.setProgress(numOfPhotos, index, false);
                    nm.notify(notifyId, builder.build());
                    try {
                        Thread.sleep(1000);
                    } catch (InterruptedException e) {
                        System.out.println("progress bar thread interrupted");
                    }

                }
                builder.setProgress(0, 0, false);
                builder.setContentText("finnished");
                nm.notify(notifyId, builder.build());
            }
        }).start();
    }

    @Override
    public void onCreate() {
        super.onCreate();
        this.intentFilter.addAction("android.net.wifi.supplicant.CONNECTION_CHANGE");
        this.intentFilter.addAction("android.net.wifi.STATE_CHANGE");
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Toast.makeText(this, "Service started", Toast.LENGTH_LONG).show();
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
        this.registerReceiver(this.receiver, intentFilter);
        this.client = new Client();
        this.client.connectToServer(8001);
        //startTransfer();
        return START_STICKY;
    }

    public void startTransfer() {
        File dcim = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM+"/Camera/");
        if (dcim == null) {
            return;
        }

        File[] pics = dcim.listFiles();
        this.index = 0;
        this.numOfPhotos = pics.length;

        if (pics == null) {
            return;
        }
        int count = 0;

        displayNotofication();
        for(File pic : pics) {
            String a = pic.getName().toString();
            sendPhotosToService(pic);
            this.index++;
        }

    }


    public boolean sendPhotosToService (File pic) {
        try {
            //foreach (File picture )
            FileInputStream fis = new FileInputStream(pic);
            Bitmap bm = BitmapFactory.decodeStream(fis);
            ByteArrayOutputStream stream = new ByteArrayOutputStream();
            bm.compress(Bitmap.CompressFormat.PNG, 70, stream);
            byte[] imgBytes = stream.toByteArray();
            int a = pic.getName().length();
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

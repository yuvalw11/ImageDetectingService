package com.example.yuvalw11.imageserviceforandroid;

import android.os.AsyncTask;
import android.util.Log;

import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;

public class Client{
    private Socket sock;



   public boolean connectToServer(int port) {

        class connectToServerTask extends AsyncTask<Integer, Void,Boolean> {

           protected Boolean doInBackground(Integer... ports) {
               try {
                   InetAddress addr = InetAddress.getByName("10.0.2.2");
                   sock = new Socket(addr, ports[0]);
                   return true;

               } catch (Exception e) {
                   Log.e("TCP", "error socket", e);
                   return false;
               }
           }
       }
       try {
           return new connectToServerTask().execute(8001).get();
       } catch (Exception e) {
            return false;
       }

   }

   public boolean sendBytes(byte[] photobytes, byte[] photoName) {
       try {
           OutputStream output = sock.getOutputStream();
           //writing to stream photo name
           output.write(photoName);
           output.flush();
           InputStream inputStream = sock.getInputStream();
           byte[] confirmation = new byte[1];
           //confirmation check
           if (inputStream.read(confirmation) == 1) {
               //send photo size
               output.write(photobytes.length);
           }
           output.flush();
           //co
           confirmation = new byte[1];
           //confirmation byte check
           if (inputStream.read(confirmation) == 1) {
               //sends the photo bytes.
               output.write(photobytes);
            }
            output.flush();
           return true;

       } catch (Exception e) {
           Log.e("TCP", "error socket", e);
           return false;
       }
   }

   public boolean closeConnection () {
       try {
           this.sock.close();
           return true;
       } catch (Exception e) {
           return false;
       }

   }




}

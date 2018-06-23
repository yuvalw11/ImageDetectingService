package com.example.yuvalw11.imageserviceforandroid;

import android.os.AsyncTask;
import android.util.Log;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

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

   public void sendBytes(final byte[] photobytes, final byte[] photoName) throws IOException {
       int a = photoName.length;
       int b = photobytes.length;
       /*ByteArrayOutputStream bos = new ByteArrayOutputStream();
       DataOutputStream dos = new DataOutputStream(bos);
       dos.writeInt(photoName.length);
       dos.flush();
       byte [] arr = bos.toByteArray();*/
       ByteBuffer cc = ByteBuffer.allocate(4);
       cc.order(ByteOrder.LITTLE_ENDIAN);
       cc.putInt(photobytes.length);
       byte [] d = cc.array();
           new Thread(new Runnable() {
               @Override
               public void run() {
                   try {
                       byte [] namelength = new byte[4];
                       OutputStream output = sock.getOutputStream();
                       //ByteArrayOutputStream bos = new ByteArrayOutputStream();
                       //DataOutputStream dos = new DataOutputStream(bos);
                       //dos.writeInt(photoName.length);
                       //dos.flush();
                       //byte [] arr = bos.toByteArray();
                       InputStream inputStream = sock.getInputStream();
                       ByteBuffer bb = ByteBuffer.allocate(4);
                       bb.order(ByteOrder.LITTLE_ENDIAN);
                       bb.putInt(photoName.length);
                       //writing to stream photo name
                       //output.write(photoName);
                        System.out.println("Before first write");
                       //output.write(bos.toByteArray());
                       output.write(bb.array());
                       output.flush();
                       byte[] confirmation = new byte[1];
                       //confirmation check
                       if (inputStream.read(confirmation) == 1) {
                           //send photo size
                           output.write(photoName);
                       }
                       output.flush();
                       //co
                       confirmation = new byte[1];
                       //confirmation byte check
                       if (inputStream.read(confirmation) == 1) {
                           String a;
                       }
                           //sends the photo length.
                           /*ByteBuffer cc = ByteBuffer.allocate(4);
                           cc.order(ByteOrder.LITTLE_ENDIAN);
                           cc.putInt(photobytes.length);
                           output.write(cc.array());
                       }
                       output.flush();
                       if (inputStream.read(confirmation) == 1) {
                           //sends the photo bytes.
                           output.write(photobytes);
                       }
                       output.flush();*/
                           String imageSize = photobytes.length+"";
                       System.out.println("Size of img is" +photobytes.length);
                       byte [] size = imageSize.getBytes();
                       output.write(size.length);
                       output.flush();
                       output.write(size);
                       output.flush();
                       output.write(photobytes);
                       output.flush();

                   } catch (Exception e) {
                       Log.e("TCP", "error socket", e);
                   }
                   }
           }).start();

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

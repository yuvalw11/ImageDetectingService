using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.Controller;
using System.Net.Sockets;
using Infrustructure;
using System.IO;
using System.Drawing;
using ImageService.ImageService.Infrastructure;

namespace ImageService.ImageService
{
    public class TCPHandler: ITCPHandler
    {
        private ImageController imageController;
        private ILoggingService ils;

        public TCPHandler (ImageController imageController, ILoggingService ils)
        {
            this.imageController = imageController;
            this.ils = ils;
        }

        //handles the client 
        public void HandleTcpClient(TcpClient client, List<TcpClient> clients)
        {
            try
            {
                new Task(() =>
                {
                    try
                    {
                        while (true)
                        {
                            ils.Log("beginning to transfer photos...", MessageTypeEnum.INFO);
                            NetworkStream stream = client.GetStream();
                            byte[] imgNameSizeBuffer = new byte[1];
                            
                            stream.Read(imgNameSizeBuffer, 0, 1);
                            /*for (int i=0;i<imgNameSizeBuffer.Length;i++)
                            {
                                int a = imgNameSizeBuffer[i];
                            }*/
                            int nameSize = BitConverter.ToInt32(imgNameSizeBuffer, 0);
                            if (nameSize == 0)
                            {
                                break;
                            }
                            

                            byte[] imgNameBuffer = new byte[nameSize];
                            stream.Read(imgNameBuffer, 0, nameSize);
                            string imgName = System.Text.Encoding.Default.GetString(imgNameBuffer);


                            byte[] imgSizesizeBuffer = new byte[1];
                            stream.Read(imgSizesizeBuffer, 0, 1);
                            int imgSizesize = BitConverter.ToInt32(imgSizesizeBuffer, 0);

                            byte[] imgSizeBuffer = new byte[imgSizesize];
                            stream.Read(imgSizeBuffer, 0, imgSizesize);
                            int imgSize = BitConverter.ToInt32(imgSizeBuffer, 0);

                            /*for (int i=0; i<imgSizeBuffer.Length;i++)
                            {
                                int a = imgSizeBuffer[i];
                            }*/

                            byte[] imgBuffer = new byte[imgSize];
                            stream.Read(imgBuffer, 0, imgSize);
                            MemoryStream ms = new MemoryStream(imgBuffer);
                            Image image = Image.FromStream(ms);

                            stream.WriteByte(1);
                            stream.Flush();

                            string url = ReadAppConfig.ReadSetting("Handlers").Split(';')[0] + "\\" + imgName;
                            image.Save(url);

                        }
                    }
                    //removes and closes client upon exception
                    catch (Exception e)
                    {
                        clients.Remove(client);
                        //display to the logger the exception that occured
                        ils.Log(e.ToString(), MessageTypeEnum.FAIL);
                        client.Close();
                    }

                }).Start();
            }
            catch (Exception ex)
            {
                ils.Log(ex.ToString(), MessageTypeEnum.FAIL);

            }
        }

        //should read from stream the image name
        /*private string GetImageName(NetworkStream stream)
        {

        }
        //should read from stream the image bytes
        private List<Byte> GetImageBytes(NetworkStream stream)
        {

        }*/
    }
}

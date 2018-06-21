using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.Controller;
using System.Net.Sockets;
using Infrustructure;
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
                            //string imageName = GetImageName(stream); //read image name
                            //List<Byte> finalbytes = GetImageBytes(stream); // read image bytes

                            //need to save the image
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

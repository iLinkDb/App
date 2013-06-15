using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppCommon
{
   public class HtmlLogFile
   {
      public bool AutoCloseBrowser { get; set; }
      public string FilePath { get; set; }
      public string FileName { get; private set; }

      private FileStream _fs;
      private TextWriter _tw;

      public HtmlLogFile(string rootPath)
      {
         FileName = "default.htm";
         AutoCloseBrowser = false;

         string testFolder = "Test_" + DateTime.Now.ToString("s").Replace(':', '-');
         FilePath = Path.Combine(rootPath, testFolder);
         Directory.CreateDirectory(FilePath);

         FileName = Path.Combine(FilePath, FileName);

         _fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 1024,
                                                FileOptions.WriteThrough | FileOptions.Asynchronous );

         _tw = new StreamWriter(_fs);
         WritePageHeader("Watin Tests (" + DateTime.Now.ToShortTimeString() + ")");
      }

      #region Page Methods

      public void WritePageHeader(string pageTitle)
      {
         _tw.WriteLine("<html>");
         _tw.WriteLine("<head>");
         _tw.WriteLine("<title>" + pageTitle + "</title>");

         _tw.WriteLine("<style type='text/css'>");
         _tw.WriteLine("<!--");
         _tw.WriteLine("th {background: #ffc;} ");
         _tw.WriteLine(".success {color: White; background: Green;} ");
         _tw.WriteLine(".error {color: White; background: Red;} ");

         _tw.WriteLine("a:link {color: White;}");
         _tw.WriteLine("a:visited {color: White;}");

         _tw.WriteLine("-->");
         _tw.WriteLine("</style>");

         _tw.WriteLine("</head>");
         _tw.WriteLine("<body>");
         _tw.WriteLine("<h2>" + pageTitle + "</h2>");
         _tw.WriteLine("<h3>Run Date: " + DateTime.Now.ToString() + "</h3>");
         _tw.Flush();
      }

      public void WritePageFooter()
      {
         _tw.WriteLine("</body>");
         _tw.WriteLine("</html>");
         _tw.Close();
      }

      public void CaptureScreen(string name)
      {
         // Give the screen a final moment to finish drawing...
         System.Threading.Thread.Sleep(1000);

         Bitmap bmpScreenshot;

         Graphics gfxScreenshot;

         // Set the bitmap object to the size of the screen
         bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,
                                    PixelFormat.Format32bppArgb);

         // Create a graphics object from the bitmap
         gfxScreenshot = Graphics.FromImage(bmpScreenshot);

         // Take the screenshot from the upper left corner to the right bottom corner
         gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,
                                      Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

         // Save the screenshot to the specified path that the user has chosen
         string fileName = name + ".jpg";
         string fullFilename = FilePath + "\\" + fileName;
         bmpScreenshot.Save(fullFilename, ImageFormat.Jpeg);
      }

      #endregion

      #region Test Methods

      internal void WriteStartTest(string testText)
      {
         _tw.WriteLine("<table>");

         _tw.WriteLine("<tr>");
         _tw.WriteLine("<th colspan='2' align='left'><h2>" + testText + "</h2></th>");
         _tw.WriteLine("</tr>");

         _tw.WriteLine("<tr>");
         _tw.WriteLine("<th>Time</th>");
         _tw.WriteLine("<th align='left'>Message</th>");
         _tw.WriteLine("</tr>");
         _tw.Flush();
      }

      internal void WriteRow(string message)
      {
         WriteRow(message, "");
      }

      internal void WriteRow(string message, string cssClass)
      {
         string time = DateTime.Now.ToString("HH:mm:ss");

         StringBuilder sb = new StringBuilder();

         sb.Append("<tr>");
         sb.AppendFormat("<td>{0}</td>", time);

         string workCss = "";
         if (!string.IsNullOrEmpty(cssClass))
         {
            workCss = string.Format(" class='{0}'", cssClass);
         }

         sb.AppendFormat("<td {0}>{1}</td>", workCss, message);
         sb.Append("</tr>");

         _tw.WriteLine(sb.ToString());
         _tw.Flush();
      }

      internal void WriteSection(string testName)
      {
         _tw.WriteLine("<h2 class='testName'>" + testName + "</h2>");
         _tw.Flush();
      }

      internal void WriteEndTest()
      {
         _tw.WriteLine("</table>");
         _tw.Flush();
      }

      #endregion


   }
}

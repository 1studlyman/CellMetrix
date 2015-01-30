using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

   

namespace CellMetrix
{
   /******************************************************************************************
   * SlideSet Object
   * 
   *    
   ******************************************************************************************/
   public class SlideSet : ObservableCollection<Slide>
   {
      public SlideSet()
      {
         
      }

      public void addSlide(Slide newSlide)
      {
         Add(newSlide);
      }
   }

   /******************************************************************************************
   * Slide Object
   * 
   *    
   ******************************************************************************************/
   public class Slide
   {
      private Image image;
      private string fileName;
      private string name;


      public Slide(string pFileName)
      {
         image = new Image();
         BitmapImage bitMapSource = new BitmapImage();

         bitMapSource.BeginInit();
         bitMapSource.UriSource = new Uri(pFileName, UriKind.Relative);
         bitMapSource.CacheOption = BitmapCacheOption.OnDemand;
         bitMapSource.EndInit();

         image.Source = bitMapSource;
         image.Stretch = Stretch.Uniform;

         fileName = pFileName;
         name = pFileName;
      }
   }

   /******************************************************************************************
    * Main Window
    * 
    *    Interaction logic for the main window.
    ******************************************************************************************/    
   public partial class MainWindow : Window
   {
      public SlideSet slideSet;

      public MainWindow()
      {
         InitializeComponent();
         this.DataContext = this;   //Sets the data context of the window to the current window

         slideSet = new SlideSet();


         //Bind the UI to the data objects here
         this.SlideSetListBox.ItemsSource = slideSet;
      }



      /******************************************************************************************
       * Event Handlers
       * 
       *    Event Handlers for the different interface elements of the UI
       ******************************************************************************************/      
      private void Open_Files_Button_Click(object sender, RoutedEventArgs e)
      {
         // Configure the open file dialogue box
         Microsoft.Win32.OpenFileDialog openDialogue = new Microsoft.Win32.OpenFileDialog();
         openDialogue.Title  = "Open Cell Image(s)";
         openDialogue.Filter = "Image Files (*.jpg *.bmp *.tif)|*.jpg;*.bmp;*.tif";


         //Show the dialogue
         Nullable<bool> isOK = openDialogue.ShowDialog();
         if (isOK.HasValue)
         {
            if ((bool)isOK)
            {
               Open_Files(openDialogue.FileNames);
            }
         }
      }



      /******************************************************************************************
       * OpenFiles(string[])
       * 
       * Opens the files given by the Open_Files_Button_Click() Event Handler
       *****************************************************************************************/
      private void Open_Files(string[] fileNames)
      {  
         foreach (string fileName in fileNames)
         {
            Slide newSlide = new Slide(fileName);
            slideSet.addSlide(newSlide);
         }
         
      }
   }
}

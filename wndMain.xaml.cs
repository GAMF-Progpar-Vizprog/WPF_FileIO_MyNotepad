using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyNotepad
{
	public partial class wndMain : Window
  {
  
    bool HasChanged;
    string FileName = "something.txt";
    public wndMain()
    {
      InitializeComponent();
      
      CommandBinding cbSave = new CommandBinding(
        ApplicationCommands.Save);
      cbSave.Executed += cbSave_Executed;
			CommandBinding cbSaveAs = new CommandBinding(
				ApplicationCommands.SaveAs);
			cbSaveAs.Executed += cbSaveAs_Executed;
			CommandBinding cbOpen =
        new CommandBinding(ApplicationCommands.Open);
      cbOpen.Executed += cbOpen_Executed;
      CommandBinding cbNew = new CommandBinding(
          ApplicationCommands.New);
      cbNew.Executed += cbNew_Executed;
      
      CommandBindings.Add(cbSave);
			CommandBindings.Add(cbSaveAs);
			CommandBindings.Add(cbOpen);
      CommandBindings.Add(cbNew);
    }

		private void cbSaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
		{
      var sfdSave = new Microsoft.Win32.SaveFileDialog();
      sfdSave.DefaultExt = ".txt";
      sfdSave.Filter = "Text files|*.txt|All files|*.*";
      sfdSave.InitialDirectory = Directory.GetCurrentDirectory();
			if (sfdSave.ShowDialog() == true)
      {
        FileName = sfdSave.FileName;
        Save();
      }
		}

		void cbNew_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      if (HasChanged)
      {
        MessageBoxResult res = MessageBox.Show("Save text?", "Save?",
          MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (res == MessageBoxResult.Yes)
          Save();
      }
      
      tbText.Text = "";
      HasChanged = false;
    }

   
    private void Save()
    {
      
      using (StreamWriter sw = new StreamWriter(FileName))
      {
        sw.Write(tbText.Text);
      }
      HasChanged = false;
      MessageBox.Show("Saved!");
    }

    void cbOpen_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      if (HasChanged)
      {
        MessageBoxResult res = MessageBox.Show("Save text?", "Save?",
          MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (res == MessageBoxResult.Yes)
          Save();
      }
			var ofdOpen = new Microsoft.Win32.OpenFileDialog();
			ofdOpen.DefaultExt = ".txt";
			ofdOpen.Filter = "Text files|*.txt|All files|*.*";
			ofdOpen.InitialDirectory = Directory.GetCurrentDirectory();
      if (ofdOpen.ShowDialog() == true)
      {
        FileName = ofdOpen.FileName;
        StreamReader sr = new StreamReader(FileName);
        tbText.Text = sr.ReadToEnd();
        sr.Close();
        HasChanged = false;
      }
    }

    void cbSave_Executed(object sender, ExecutedRoutedEventArgs e)
    {
      Save();
    }

    private void miExit_Click(object sender, RoutedEventArgs e)
    {
      if (HasChanged)
      {
        MessageBoxResult res = MessageBox.Show("Save text?", "Save?",
          MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (res == MessageBoxResult.Yes)
          Save();
      }
      Application.Current.Shutdown();
    }

    private void tbText_TextChanged(object sender, TextChangedEventArgs e)
    {
      HasChanged = true;
    }
  }

}

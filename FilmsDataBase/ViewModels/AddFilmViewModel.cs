using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using FilmsDataBase.Services;
using System.Windows;
using System.Windows.Forms;
using System;

namespace FilmsDataBase.ViewModels
{
  
  internal class AddFilmViewModel : ViewModel
  {
    private void NotificationFileSaved()
    {
      System.Windows.MessageBox.Show(
        "File saved",
        "Notification",
        (MessageBoxButton)MessageBoxButtons.OK,
        (MessageBoxImage)MessageBoxIcon.Information,
        (MessageBoxResult)MessageBoxDefaultButton.Button1,
        System.Windows.MessageBoxOptions.DefaultDesktopOnly);
    }
    private MessageBoxResult Alarm(string message, string caption, MessageBoxButton button, MessageBoxImage icon) =>
      System.Windows.MessageBox.Show(message, caption, button, icon);

    #region Properties
    private DisplayRootRegistry _displayRootRegistry;
    public DisplayRootRegistry DisplayRootRegistry
    {
      set { _displayRootRegistry = value; }
      get { return _displayRootRegistry; }
    }
    private string _title;
    public string Title { get => _title; set => Set(ref _title, value); }

    private string _description;
    public string Description { get => _description; set => Set(ref _description, value); }

    private string _icon;
    public string Icon { get => _icon; set => Set(ref _icon, value); }

    private string _trailer;
    public string Trailer { get => _trailer; set => Set(ref _trailer, value); }

    private DateTime _year;
    public DateTime Year { get => _year; set => Set(ref _year, value); }
    //private int _intYear;
    //public int IntYear { get => _intYear; set => Set(ref _intYear, value); }
    private string _oldTitle;
    public string OldTitle { get => _oldTitle; set => Set(ref _oldTitle, value); }
    #endregion

    #region Commands

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      if (Alarm("Are you sure you want to close window?", "Word Processor", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        DisplayRootRegistry.HidePresentation(this);
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion

    #region SaveDataCommand
    public ICommand SaveDataCommand { get; }
    private void OnSaveDataCommandExecuted(object p)
    {
      Functionality functionality = new Functionality();
      if (functionality.IsEmpty() || !functionality.Exist(OldTitle))
        functionality.SetData(Title, Description, Icon, Trailer, Year);
      else
        functionality.UpdataDataBase(OldTitle, Title, Description, Icon, Trailer, Year);
      NotificationFileSaved();
    }
    private bool CanSaveDataCommandExecute(object p) 
    {
      if (Title == string.Empty || Title == null) return false;
      if (Description == string.Empty || Description == null) return false;
      if (Icon == string.Empty || Icon == null) return false;
      if (Trailer == string.Empty || Trailer == null) return false;
      return true;
    }
    #endregion

    #region AddIconCommand
    public ICommand AddIconCommand { get; }
    private void OnAddIconCommandExecuted(object p)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";
      openFileDialog.InitialDirectory = "D:\\4 семестр\\РПС\\FilmsDataBase\\";
      if(openFileDialog.ShowDialog() == DialogResult.OK)
      {
        var filePath = openFileDialog.FileName;
        Icon = filePath;
      }
    }
    private bool CanAddIconCommandExecute(object p) => true;
    #endregion

    #region AddTraileCommand
    public ICommand AddTrailerCommand { get; }
    private void OnAddTrailerCommandExecuted(object p)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "All Media Files|*.wav;*.aac;*" +
        ".wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;" +
        "*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;" +
        "*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;" +
        "*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;" +
        "*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;" +
        "*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV"; ;
      openFileDialog.InitialDirectory = "D:\\4 семестр\\РПС\\FilmsDataBase\\";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        var filePath = openFileDialog.FileName;
        Trailer = filePath;
      }
    }
    private bool CanAddTrailerCommandExecute(object p) => true;
    #endregion

    #endregion

    public AddFilmViewModel()
    {
      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      SaveDataCommand = new LambdaCommand(OnSaveDataCommandExecuted, CanSaveDataCommandExecute);
      AddIconCommand = new LambdaCommand(OnAddIconCommandExecuted, CanAddIconCommandExecute);
      AddTrailerCommand = new LambdaCommand(OnAddTrailerCommandExecuted, CanAddTrailerCommandExecute);
      #endregion
    }
  }
}

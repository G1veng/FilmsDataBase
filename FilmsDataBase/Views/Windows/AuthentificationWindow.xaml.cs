using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using FilmsDataBase.ViewModels;

namespace FilmsDataBase.Views.Windows
{
  /// <summary>
  /// Interaction logic for AuthentificationWindow.xaml
  /// </summary>
  public partial class AuthentificationWindow : Window
  {
    public AuthentificationWindow()
    {
      InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
      if(this.DataContext != null)
      {
        ((AuthentificationViewModel)this.DataContext).Password = ((PasswordBox)sender).Password;
      }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (!((AuthentificationViewModel)this.DataContext).IsRegistrated)
      {
        Environment.Exit(0);
      }
    }
  }
}

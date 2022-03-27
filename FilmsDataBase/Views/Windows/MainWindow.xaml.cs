using System.ComponentModel;
using System.Windows;

namespace FilmsDataBase
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    /*<TextBlock Text = "{Binding Films[0].Title}" TextWrapping="Wrap" Grid.Column="0" Grid.Row="0" Margin="10, 60, 10, 40" TextAlignment="Center"/>
        <TextBlock Text = "{Binding Films[0].Year}" Margin="0, 100, 0, 0" HorizontalAlignment="Center" Grid.Row="0"/>
        <TextBlock Text = "{Binding Films[0].Description}" TextWrapping="Wrap" Grid.Column="1" Grid.Row="0" Margin="10, 20, 10, 10" HorizontalAlignment="Center" TextAlignment="Justify"/>
        <Image Source = "{Binding Films[0].Icon}" Margin="10" Grid.Column="3" Grid.RowSpan="3"/>
        <MediaElement Source = "{Binding Films[0].Trailer}" Grid.Row="1" Grid.ColumnSpan="2" LoadedBehavior="Stop" Margin="0, 0, 0, 10" Grid.RowSpan="2"/>*/
    /*<DataGridTextColumn Header = "Ttile" Width="80" Binding="{Binding Source=Films, Path=Title}"/>
                <DataGridTextColumn Header = "Description" Width="200" Binding="{Binding Source=Films, Path=Description}"/>
                <DataGridTemplateColumn Header = "Icon" Width="120" CellTemplate="{StaticResource Icon}"/>
                <DataGridTemplateColumn Header = "Trailer" Width="240" CellTemplate="{StaticResource Trailer}"/>
                <DataGridTextColumn Header = "Year" Width="80" Binding="{Binding Source=Films, Path=Year}"/>*/
    public MainWindow()
    {
      InitializeComponent();
    }
  }
}

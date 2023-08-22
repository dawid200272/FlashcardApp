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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlashcardApp.WPF.Controls;
/// <summary>
/// Interaction logic for DeckNameForm.xaml
/// </summary>
public partial class DeckNameForm : UserControl
{
    public static readonly DependencyProperty SubmitCommandProperty =
        DependencyProperty.Register("SubmitCommand", typeof(ICommand), typeof(DeckNameForm), new PropertyMetadata(null));

    public ICommand SubmitCommand
    {
        get { return (ICommand)GetValue(SubmitCommandProperty); }
        set { SetValue(SubmitCommandProperty, value); }
    }

    public static readonly DependencyProperty CloseCommandProperty =
        DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(DeckNameForm), new PropertyMetadata(null));

    public ICommand CloseCommand
    {
        get { return (ICommand)GetValue(CloseCommandProperty); }
        set { SetValue(CloseCommandProperty, value); }
    }

    public static readonly DependencyProperty DeckNameProperty =
        DependencyProperty.Register("DeckName", typeof(string), typeof(DeckNameForm), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string DeckName
    {
        get { return (string)GetValue(DeckNameProperty); }
        set { SetValue(DeckNameProperty, value); }
    }
    
    public static readonly DependencyProperty CanSubmitProperty =
        DependencyProperty.Register("CanSubmit", typeof(bool), typeof(DeckNameForm), new FrameworkPropertyMetadata(false));

    public bool CanSubmit
    {
        get { return (bool)GetValue(CanSubmitProperty); }
        set { SetValue(CanSubmitProperty, value); }
    }

    public DeckNameForm()
    {
        InitializeComponent();
    }
}

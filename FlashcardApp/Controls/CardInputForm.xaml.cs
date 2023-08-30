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
/// Interaction logic for CardInputForm.xaml
/// </summary>
public partial class CardInputForm : UserControl
{
    public static readonly DependencyProperty SubmitCommandProperty =
        DependencyProperty.Register("SubmitCommand", typeof(ICommand), typeof(CardInputForm), new PropertyMetadata(null));

    public ICommand SubmitCommand
    {
        get { return (ICommand)GetValue(SubmitCommandProperty); }
        set { SetValue(SubmitCommandProperty, value); }
    }

    public static readonly DependencyProperty CloseCommandProperty =
        DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(CardInputForm), new PropertyMetadata(null));

    public ICommand CloseCommand
    {
        get { return (ICommand)GetValue(CloseCommandProperty); }
        set { SetValue(CloseCommandProperty, value); }
    }

    public static readonly DependencyProperty SubmitButtonTextProperty =
        DependencyProperty.Register("SubmitButtonText", typeof(string), typeof(CardInputForm), new FrameworkPropertyMetadata(string.Empty));

    public string SubmitButtonText
    {
        get { return (string)GetValue(SubmitButtonTextProperty); }
        set { SetValue(SubmitButtonTextProperty, value); }
    }

    public static readonly DependencyProperty CloseButtonTextProperty =
        DependencyProperty.Register("CloseButtonText", typeof(string), typeof(CardInputForm), new FrameworkPropertyMetadata(string.Empty));

    public string CloseButtonText
    {
        get { return (string)GetValue(CloseButtonTextProperty); }
        set { SetValue(CloseButtonTextProperty, value); }
    }

    public static readonly DependencyProperty FrontProperty =
        DependencyProperty.Register("Front", typeof(string), typeof(CardInputForm), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string Front
    {
        get { return (string)GetValue(FrontProperty); }
        set { SetValue(FrontProperty, value); }
    }

    public static readonly DependencyProperty BackProperty =
        DependencyProperty.Register("Back", typeof(string), typeof(CardInputForm), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string Back
    {
        get { return (string)GetValue(BackProperty); }
        set { SetValue(BackProperty, value); }
    }

    public static readonly DependencyProperty CanSubmitProperty =
        DependencyProperty.Register("CanSubmit", typeof(bool), typeof(CardInputForm), new FrameworkPropertyMetadata(false));

    public bool CanSubmit
    {
        get { return (bool)GetValue(CanSubmitProperty); }
        set { SetValue(CanSubmitProperty, value); }
    }

    public CardInputForm()
    {
        InitializeComponent();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication1.Controls
{
    public partial class TypeAheadHighlightningControl : UserControl
    {
        public TypeAheadHighlightningControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TypeAheadHighlightningControlProperty = DependencyProperty.Register("HighlightedText", typeof(string), typeof(TypeAheadHighlightningControl), new PropertyMetadata(string.Empty, PropertyChangedCallback));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TypeAheadHighlightningControl), new PropertyMetadata(string.Empty, PropertyChangedCallback));
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(string), typeof(TypeAheadHighlightningControl), new PropertyMetadata(null));
        public static readonly DependencyProperty ShowImageProperty = DependencyProperty.Register("ShowImage", typeof(bool), typeof(TypeAheadHighlightningControl), new PropertyMetadata(null));

        public string HighlightedText
        {
            get { return (string)GetValue(TypeAheadHighlightningControlProperty); }
            set { SetValue(TypeAheadHighlightningControlProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public bool ShowImage
        {
            get { return (bool)GetValue(ShowImageProperty); }
            set { SetValue(ShowImageProperty, value); }
        }


        private static void PropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBlock = sender as TypeAheadHighlightningControl;
            textBlock.BuildText();
        }

        private void BuildText()
        {
            ResultText.Inlines.Clear();

            var parts = AutoCompleteHighlightingSplitter.Split(Text, HighlightedText);

            foreach (var part in parts)
            {
                if (part.IsHighlighted)
                {
                    HighlightedTextStyle(ResultText, part.Text);
                }
                else
                {
                    NormalTextStyle(ResultText, part.Text);
                }
            }
        }

        private void HighlightedTextStyle(TextBlock textBlock, string text)
        {
            Run run = new Run
            {
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Blue),  //Application.Current.Resources["GspMediumBlueBrush"] as Brush,
                Text = text
            };
            textBlock.Inlines.Add(run);
        }

        private void NormalTextStyle(TextBlock textBlock, string text)
        {
            Run run = new Run { Text = text };
            textBlock.Inlines.Add(run);
        }
    }

    public class AutoCompleteHighlightingSplitPart
    {
        public string Text { get; private set; }
        public bool IsHighlighted { get; private set; }

        public AutoCompleteHighlightingSplitPart(string text, bool isHighlighted)
        {
            Text = text;
            IsHighlighted = isHighlighted;
        }
    }

    public static class AutoCompleteHighlightingSplitter
    {
        public static IEnumerable<AutoCompleteHighlightingSplitPart> Split(string text, string value)
        {
            List<AutoCompleteHighlightingSplitPart> parts = new List<AutoCompleteHighlightingSplitPart>();

            if (string.IsNullOrEmpty(text))
            {
                return parts;
            }

            if (string.IsNullOrEmpty(value))
            {
                var part = new AutoCompleteHighlightingSplitPart(text, false);
                parts.Add(part);
                return parts;
            }

            SplitByContains(text, value, StringComparison.CurrentCultureIgnoreCase, parts);

            return parts;
        }

        private static void SplitByContains(string text, string value, StringComparison stringComparison, IList<AutoCompleteHighlightingSplitPart> parts)
        {
            int index = text.ReplaceDiacritics().IndexOf(value.ReplaceDiacritics(), stringComparison);

            if (index >= 0)
            {
                while (index >= 0)
                {
                    if (index > 0)
                    {
                        var part = new AutoCompleteHighlightingSplitPart(text.Substring(0, index), false);
                        parts.Add(part);
                    }

                    var matchText = text.Substring(index, value.Length);
                    var matchPart = new AutoCompleteHighlightingSplitPart(matchText, true);
                    parts.Add(matchPart);

                    text = text.Substring(index + value.Length);
                    index = text.IndexOf(value, stringComparison);
                }

                if (text.Length > 0)
                {
                    var part = new AutoCompleteHighlightingSplitPart(text, false);
                    parts.Add(part);
                }
            }
            else
            {
                var part = new AutoCompleteHighlightingSplitPart(text, false);
                parts.Add(part);
            }
        }

        //Simple (and not full) solution since normalize does not seem to exist for silverlight...
        public static string ReplaceDiacritics(this string s)
        {
            var noDiacritics = s;

            noDiacritics = noDiacritics.NeverMindCase("Å", "A");
            noDiacritics = noDiacritics.NeverMindCase("Ä", "A");
            noDiacritics = noDiacritics.NeverMindCase("Ö", "O");
            noDiacritics = noDiacritics.NeverMindCase("Æ", "A");
            noDiacritics = noDiacritics.NeverMindCase("Í", "I");
            noDiacritics = noDiacritics.NeverMindCase("Ñ", "N");

            noDiacritics = noDiacritics.NeverMindCase("É", "E");
            noDiacritics = noDiacritics.NeverMindCase("Á", "A");
            noDiacritics = noDiacritics.NeverMindCase("Û", "U");
            noDiacritics = noDiacritics.NeverMindCase("Ü", "U");

            return noDiacritics;
        }

        public static string NeverMindCase(this string wholeWord, string replace, string with)
        {
            var returnValue = wholeWord.Replace(replace.ToLower(), with.ToLower());
            returnValue = returnValue.Replace(replace.ToUpper(), with.ToUpper());
            return returnValue;
        }
    }
}

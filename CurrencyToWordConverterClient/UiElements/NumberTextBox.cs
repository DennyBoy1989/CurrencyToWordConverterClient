using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CurrencyToWordConverterClient.UiElements;

/// <summary>
/// TextBox that only accepts number inputs. All other inputs are ignored.
/// </summary>
public class NumberTextBox : TextBox {

    private static readonly Regex numbersRegex = new Regex("\\d+");

    protected override void OnPreviewTextInput(TextCompositionEventArgs e) {

        if (!numbersRegex.IsMatch(e.Text))
            e.Handled = true;
        base.OnPreviewTextInput(e);
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e) {
        if (e.Key == Key.Space)
            e.Handled = true;
        base.OnPreviewKeyDown(e);
    }
}

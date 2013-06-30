using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommon
{
    public interface ITestEngine
    {
        bool Assert(string lookFor);
        void ButtonClickById(string fieldName);
        void Cleanup();
        void ClearCache();
        void LinkClickById(string linkId);
        void LinkClickByText(string linkText);
        void SetFocusToBody();
        void SetRadioByName(string name, int index);
        void SetSelectListById(string fieldName, string value);
        void SetSelectListByName(string fieldName, string value);
        void SetTextFieldById(string fieldName, string value);
        void SetTextFieldByName(string fieldName, string value);

        void ShowError(string message, Exception ex);
        void WriteScreenShot(string message);

    }
}

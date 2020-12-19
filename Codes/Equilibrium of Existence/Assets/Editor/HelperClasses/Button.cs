using System;
using UnityEngine;

namespace Editor.HelperClasses
{
    public class Button
    {
        private string ButtonName { get; }
        private Action Function { get; }

        public Button(string buttonText, Action executeFunction)
        {
            ButtonName = buttonText;
            Function = executeFunction;
        }

        public void Draw()
        {
            if (GUILayout.Button(ButtonName)) Function.Invoke();
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLooper
{
    [Serializable]
    public class TestLooperAttribute : TestClassExtensionAttribute
    {

        public static string LOOPER_CONTEXT_PROPERTY = "LooperObject";
        /*  I want this to be able to pass a property name to the attribute to tell it what property to look for in the context.  For now, I will just hard code it as a static
        private string _PropertyName;
        public string PropertyName
        {
            get
            { return _PropertyName; }
            set
            {
                _PropertyName = value;
            }
        }
         */ 
        public override Uri ExtensionId
        {

            get
            {
                return new Uri("urn:TestLooperAttribute");
            }
        }

        public override TestExtensionExecution GetExecution()
        {

            return new TestLooperExecution(LOOPER_CONTEXT_PROPERTY);
        }

        /*
         * Attempts to override instancing
         * 
        public override int GetHashCode()
        {
            return _PropertyName.GetHashCode();
        }
        public override bool Match(object obj)
        {
            var potentialMatch = obj as TestLooperAttribute;
            if (potentialMatch != null)
                return potentialMatch.PropertyName == _PropertyName;

            return false;
        }

       
        public override bool Equals(object other)
        {
            return Match(other);
        }
        */
    }
}

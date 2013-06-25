using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLooper
{
    class TestLooperInvoker : ITestMethodInvoker
    {
        private TestMethodInvokerContext m_invokerContext;
        private string PropertyName;

        public TestLooperInvoker(TestMethodInvokerContext InvokerContext, string PropertyName)
        {
            m_invokerContext = InvokerContext;
            this.PropertyName = PropertyName;
        }

        public TestMethodInvokerResult Invoke(params object[] args)
        {
            
            // Our helper results class to aggregate our test results
            HelperTestResults results = new HelperTestResults();

            IEnumerable<object> objects = m_invokerContext.TestContext.Properties[PropertyName] as IEnumerable<object>;

            foreach (var d in objects)
                results.AddTestResult(m_invokerContext.InnerInvoker.Invoke(d), new object[1] { d.GetType().ToString()});

            var output = results.GetAllResults();
            m_invokerContext.TestContext.WriteLine(output.ExtensionResult.ToString());

            return output;
        }
    }
}

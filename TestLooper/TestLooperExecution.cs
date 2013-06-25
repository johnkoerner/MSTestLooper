using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLooper
{
class TestLooperExecution : TestExtensionExecution
{
    private string PropertyName;
        
    public TestLooperExecution(string PropertyName)
    {
        this.PropertyName = PropertyName;
    }

    public override ITestMethodInvoker CreateTestMethodInvoker(TestMethodInvokerContext InvokerContext)
    {
        return new TestLooperInvoker(InvokerContext, PropertyName);
    }

    public override void Dispose()
    {
        //TODO: Free, release or reset native resources
    }

    public override void Initialize(TestExecution Execution)
    {
        //TODO: Wire up event handlers for test events if needed

    }
}
}

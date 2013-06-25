using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLooper
{
    /// <summary>
    /// Helper Test Result class
    /// </summary>
    public class HelperTestResults
    {
        /// <summary>
        /// Member variables
        /// </summary>
        private int m_rowCount = 0;
        /// <summary>
        /// Lets us know if there is a failure somewhere in the list of results
        /// </summary>
        private bool m_hasTestFailures = false;
        /// <summary>
        /// Will be used when we provide the results for the results detail
        /// </summary>
        private StringBuilder m_resultStringBuilder = new StringBuilder();

        /// <summary>
        /// Adds a result to the list of results we have
        /// </summary>
        /// <param name="Result">The result of the iteraiton</param>
        /// <param name="RowValues">Row Values that were used for the iteraiton</param>
        public void AddTestResult(TestMethodInvokerResult Result, object[] RowValues)
        {
            m_rowCount++;
            writeResultHeader(RowValues);
            writeTestOutcome(Result);
        }


        /// <summary>
        /// Gets the full run results to return to the client
        /// </summary>
        /// <returns>TestMethodInvokerResult to return to client</returns>
        public TestMethodInvokerResult GetAllResults()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Tested {0} rows \n\n", m_rowCount);
            sb.AppendLine();
            sb.Append(m_resultStringBuilder.ToString());

            TestMethodInvokerResult tmir = new TestMethodInvokerResult();
            tmir.ExtensionResult = sb.ToString();

            if (m_hasTestFailures)
                tmir.Exception = new Exception("see test details");

            return tmir;
        }

        /// <summary>
        /// Writes failure information if iteration failed
        /// </summary>
        /// <param name="Result">failed result incl any exceptionh</param>
        private void writeFailureMessage(TestMethodInvokerResult Result)
        {
            m_resultStringBuilder.AppendLine("failed");
            Exception ex = Result.Exception;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            m_resultStringBuilder.AppendLine(ex.Message);
        }

        /// <summary>
        /// Writes the result of the iteration to the results string builder
        /// </summary>
        /// <param name="Result">The result for this iteration</param>
        private void writeTestOutcome(TestMethodInvokerResult Result)
        {
            m_resultStringBuilder.Append("Outcome: ");

            if (Result.Exception != null)
            {
                m_hasTestFailures = true;
                writeFailureMessage(Result);
            }
            else
            {
                m_resultStringBuilder.AppendLine("passed");
            }
            m_resultStringBuilder.AppendLine("--------------------------");
            m_resultStringBuilder.AppendLine();
            return;
        }

        /// <summary>
        /// writes header information to our results string builder
        /// </summary>
        /// <param name="RowValues">Row values that were used when executing the test</param>
        private void writeResultHeader(object[] RowValues)
        {
            m_resultStringBuilder.AppendFormat("Row {0} execution \n", m_rowCount);
            m_resultStringBuilder.Append("Row input values: ");
            foreach (var o in RowValues)
                m_resultStringBuilder.AppendFormat(o.ToString() + " ");

            m_resultStringBuilder.AppendLine();
        }

    }
}

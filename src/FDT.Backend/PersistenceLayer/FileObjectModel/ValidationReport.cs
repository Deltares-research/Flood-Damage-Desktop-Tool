using System.Collections.Generic;
using System.Linq;

namespace FIAT.Backend.PersistenceLayer.FileObjectModel
{
    public class ValidationReport
    {
        public IList<string> IssueList { get; private set; }

        public ValidationReport()
        {
            IssueList = new List<string>();
        }

        public virtual bool HasErrors()
        {
            return IssueList.Any();
        } 

        public void AddIssue(string issueMessage)
        {
            if (string.IsNullOrEmpty(issueMessage))
                return;
            IssueList.Add(issueMessage);
        }
    }
}
using Elsa.Services;
using System.Collections.Generic;
using Workflow.Api.Models.Dtos;
using Workflow.Api.Models.Entities;

namespace Workflow.Api.Workflow.Bookmarks
{
    public class CaseBookmarkProvider : BookmarkProvider<CaseBookmark, Case>
    {
        public override IEnumerable<BookmarkResult> GetBookmarks(BookmarkProviderContext<Case> context)
        {
            return new[] { Result(new CaseBookmark()) };
        }
    }
}

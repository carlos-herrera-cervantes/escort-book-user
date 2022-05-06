﻿using Newtonsoft.Json;

namespace EscortBookUser.Types
{
    public class Pager
    {
        #region snippet_Properties

        [JsonProperty("page")]
        public int Page { get; set; } = 1;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;

        #endregion

        #region snippet_Deconstructors

        public void Deconstruct(out int page, out int pageSize)
            => (page, pageSize) = (Page, PageSize);

        #endregion
    }
}

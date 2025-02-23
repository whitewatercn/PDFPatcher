﻿
namespace PDFPatcher.Processor
{
	sealed class BookmarkOpenStatusProcessor : IPdfInfoXmlProcessor, IPdfInfoXmlProcessor<bool>
	{
		/// <summary>
		/// 表示处理器是否应打开书签。
		/// </summary>
		public bool Parameter { get; set; }
		public BookmarkOpenStatusProcessor () {
		}
		public BookmarkOpenStatusProcessor (bool open) {
			this.Parameter = open;
		}

		#region IInfoDocProcessor 成员

		public string Name {
			get { return "设置书签状态为" + (this.Parameter ? "打开" : "关闭"); }
		}

		public IUndoAction Process (System.Xml.XmlElement item) {
			if (item.SelectSingleNode (Constants.Bookmark) == null) {
				return null;
			}
			var v = item.HasChildNodes && item.SelectSingleNode (Constants.Bookmark) != null && Parameter
				? Constants.Boolean.True
				: null;
			return UndoAttributeAction.GetUndoAction (item, Constants.BookmarkAttributes.Open, v);
		}

		#endregion

	}
}

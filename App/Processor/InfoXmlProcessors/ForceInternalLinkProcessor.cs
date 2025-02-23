﻿
using System;
using PDFPatcher.Common;
using A = PDFPatcher.Constants.ActionType;
using D = PDFPatcher.Constants.DestinationAttributes;

namespace PDFPatcher.Processor
{
	sealed class ForceInternalLinkProcessor : IPdfInfoXmlProcessor
	{
		#region IInfoDocProcessor 成员

		public string Name {
			get { return "设置点击目标到页首"; }
		}

		public IUndoAction Process (System.Xml.XmlElement item) {
			if (item.HasAttribute (D.Action)
				&& item.GetAttribute (D.Path).EndsWith (".pdf", StringComparison.InvariantCultureIgnoreCase)
				&& ValueHelper.IsInCollection (item.GetAttribute (D.Action), A.GotoR, A.Launch, A.Uri)) {
				var undo = new UndoActionGroup ();
				undo.Add (UndoAttributeAction.GetUndoAction (item, D.Action, A.Goto));
				if (item.HasAttribute (D.Page) == false
					&& item.HasAttribute (D.Named) == false
					&& item.HasAttribute (D.NamedN) == false
					) {
					undo.Add (UndoAttributeAction.GetUndoAction (item, D.Page, "1"));
				}
				return undo;
			}
			return null;
		}

		#endregion
	}
}

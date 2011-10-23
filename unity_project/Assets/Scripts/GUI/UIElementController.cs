using System;
using HappyPenguin.Controllers;

namespace HappyPenguin.UI
{
	public abstract class UIElementController<T> : Controller<UIElementBehaviour<T>>
	{
		#region implemented abstract members of HappyPenguin.Controllers.Controller[UIElementBehaviour[GUIManager]]
		protected override void UpdateOverride (UIElementBehaviour<T> entity)
		{
			// nada
		}
		
		#endregion
	}
}


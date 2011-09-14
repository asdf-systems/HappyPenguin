using UnityEngine;
using System.ComponentModel;
using System;
namespace HappyPenguin.Collections
{

public sealed class ListChangedEventArgs : EventArgs {

	public int NewIndex {
		get;
		internal set;
	}
	
	public ListChangedType ListChangedType {
		get;
		set;
	}
}}

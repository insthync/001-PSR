// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.9
// 

using Colyseus.Schema;
using Action = System.Action;

public partial class User : Schema {
	[Type(0, "string")]
	public string latestMessage = default(string);

	/*
	 * Support for individual property change callbacks below...
	 */

	protected event PropertyChangeHandler<string> _latestMessageChange;
	public Action OnLatestMessageChange(PropertyChangeHandler<string> handler) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(latestMessage));
		_latestMessageChange += handler;
		return () => {
			__callbacks.RemovePropertyCallback(nameof(latestMessage));
			_latestMessageChange -= handler;
		};
	}

	protected override void TriggerFieldChange(DataChange change) {
		switch (change.Field) {
			case nameof(latestMessage): _latestMessageChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
			default: break;
		}
	}
}


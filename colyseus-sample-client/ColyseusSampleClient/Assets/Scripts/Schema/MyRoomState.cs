// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.9
// 

using Colyseus.Schema;
using Action = System.Action;

public partial class MyRoomState : Schema {
	[Type(0, "string")]
	public string mySynchronizedProperty = default(string);

	[Type(1, "map", typeof(MapSchema<User>))]
	public MapSchema<User> users = new MapSchema<User>();

	/*
	 * Support for individual property change callbacks below...
	 */

	protected event PropertyChangeHandler<string> _mySynchronizedPropertyChange;
	public Action OnMySynchronizedPropertyChange(PropertyChangeHandler<string> handler) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(mySynchronizedProperty));
		_mySynchronizedPropertyChange += handler;
		return () => {
			__callbacks.RemovePropertyCallback(nameof(mySynchronizedProperty));
			_mySynchronizedPropertyChange -= handler;
		};
	}

	protected event PropertyChangeHandler<MapSchema<User>> _usersChange;
	public Action OnUsersChange(PropertyChangeHandler<MapSchema<User>> handler) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(users));
		_usersChange += handler;
		return () => {
			__callbacks.RemovePropertyCallback(nameof(users));
			_usersChange -= handler;
		};
	}

	protected override void TriggerFieldChange(DataChange change) {
		switch (change.Field) {
			case nameof(mySynchronizedProperty): _mySynchronizedPropertyChange?.Invoke((string) change.Value, (string) change.PreviousValue); break;
			case nameof(users): _usersChange?.Invoke((MapSchema<User>) change.Value, (MapSchema<User>) change.PreviousValue); break;
			default: break;
		}
	}
}


namespace UDT.Controllables.Touch
{
	public struct Gesture
	{
		public enum GestureType
		{
			Swipe,
			LongPress,
			Pan,
			Tap
		}
		public GestureType type;
		public float magnitude;

		public Gesture(GestureType type, float magnitude)
		{
			this.type = type;
			this.magnitude = magnitude;
		}
	}
}
// TODO: Make an Input Action Asset with a Map for Touch Inputs, considering the Touch Inputs are much more basic
// than other Input Actions
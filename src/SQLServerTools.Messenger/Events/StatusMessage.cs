using Prism.Events;

namespace SQLServerTools.Messenger.Events
{
  public class ChangeRequestLeftStatusMessageEvent : PubSubEvent<string> { }
  public class ChangeRequestCenterStatusMessageEvent : PubSubEvent<string> { }
  public class ChangeRequestRightStatusMessageEvent : PubSubEvent<string> { }
}



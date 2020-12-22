using UnityEditor;
using UnityEngine;

//An item that can react to the UI events done on an ActionItem
public interface ActionItemActionHandler {

    void onMouseClick(string actionName);
   
}

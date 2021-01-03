# TriggerLib
 A class library to create trigger event.  
  
# Memo
 Create a new triggersource.  
 ```
 var triggerSource = new TriggerSource(3000, () =>  
 {  
     MenuPanel.Visibility = Visibility.Hidden;  
 }, pullImmed: false);  
 ```
  
 Pull trigger.  
 `triggerSource.Pull();`  
  
 Reset the trigger.  
 `triggerSource.ResetTrigger(pullImmed: false);`  
  
 Get the trigger's status.  
 ```
 var ispulled = triggerSource.Trigger.IsPulled;  
 var iscountingdown = triggerSource.Trigger.IsCountingDown;  
 ```


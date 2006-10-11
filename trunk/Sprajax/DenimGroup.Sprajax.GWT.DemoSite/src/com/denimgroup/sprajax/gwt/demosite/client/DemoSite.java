package com.denimgroup.sprajax.gwt.demosite.client;

import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.core.client.GWT;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.rpc.ServiceDefTarget;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.RootPanel;
import com.google.gwt.user.client.ui.Widget;

/**
 * Entry point classes define <code>onModuleLoad()</code>.
 */
public class DemoSite
implements EntryPoint
{

  /**
   * This is the entry point method.
   */
  public void onModuleLoad()
  {
    final Button button = new Button("Click me");
    final Label label = new Label();
    final TextBox text = new TextBox();
    
    final Label label2 = new Label();
    final Button button2 = new Button("Click me too");
    final TextBox text2 = new TextBox();

    button.addClickListener(new ClickListener() {
      public void onClick(Widget sender) {
    	  
    	  String textValue = text.getText();
    	  
    	  TestDBServiceAsync dbService = (TestDBServiceAsync)GWT.create(TestDBService.class);
    	  
    	  ServiceDefTarget endpoint = (ServiceDefTarget)dbService;
    	  endpoint.setServiceEntryPoint("/testdbservice");
    	  
    	  AsyncCallback callback = new AsyncCallback() {
    		  public void onSuccess(Object result)
    		  {
    			  label.setText(result.toString());
    		  }

    		  public void onFailure(Throwable caught) {
    			  label.setText("Error occurred: " + caught);
    		  }
    	  };
    	  
    	  dbService.retrieveNameForUser(textValue, callback);
      }
    });
    
    button2.addClickListener(new ClickListener() {
        public void onClick(Widget sender) {
      	  
      	  String textValue = text2.getText();
      	  
      	  TestDBServiceAsync dbService = (TestDBServiceAsync)GWT.create(TestDBService.class);
      	  
      	  ServiceDefTarget endpoint = (ServiceDefTarget)dbService;
      	  endpoint.setServiceEntryPoint("/testdbservice");
      	  
      	  AsyncCallback callback = new AsyncCallback() {
      		  public void onSuccess(Object result)
      		  {
      			  label2.setText(result.toString());
      		  }

      		  public void onFailure(Throwable caught) {
      			  label2.setText("Error occurred: " + caught);
      		  }
      	  };
      	  
      	  dbService.retrieveUseridForUser(textValue, callback);
        }
      });  

    RootPanel.get("slot1").add(text);
    RootPanel.get("slot2").add(button);
    RootPanel.get("slot3").add(label);
    
    RootPanel.get("slot4").add(text2);
    RootPanel.get("slot5").add(button2);
    RootPanel.get("slot6").add(label2);
  }
}

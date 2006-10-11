package com.denimgroup.sprajax.gwt.demosite.client;

import com.google.gwt.user.client.rpc.AsyncCallback;

public interface TestDBServiceAsync {
	public void retrieveNameForUser(String username, AsyncCallback callback);

	public void retrieveUseridForUser(String username, AsyncCallback callback);
}
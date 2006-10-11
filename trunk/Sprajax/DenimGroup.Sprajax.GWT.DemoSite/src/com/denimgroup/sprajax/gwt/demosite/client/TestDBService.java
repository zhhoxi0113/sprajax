package com.denimgroup.sprajax.gwt.demosite.client;

import com.google.gwt.user.client.rpc.RemoteService;

public interface TestDBService
extends RemoteService
{
	public String retrieveNameForUser(String username);
	public int retrieveUseridForUser(String username);
}

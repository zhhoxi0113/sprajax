package com.denimgroup.sprajax.gwt.demosite.server;

import com.denimgroup.sprajax.gwt.demosite.client.TestDBService;
import com.google.gwt.user.server.rpc.RemoteServiceServlet;

public class TestDBServiceImpl
extends RemoteServiceServlet
implements TestDBService
{
	private static final long serialVersionUID = 101764648672761626L;

	public String retrieveNameForUser(String username)
	{
		//	TOFIX - Implement me!
		return("retrieveNameForUser parameter was '" + username + "'");
	}

	public int retrieveUseridForUser(String username)
	{
		//	TOFIX - Implement me!
		if(false)
		{
			
		}
		else
		{
			throw new RuntimeException("This message always fails and the Black Knight always triumphs!");			
		}
		
		return(-59);
	}

}

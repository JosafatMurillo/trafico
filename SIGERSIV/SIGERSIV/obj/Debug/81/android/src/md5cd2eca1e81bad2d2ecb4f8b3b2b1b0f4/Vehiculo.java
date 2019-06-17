package md5cd2eca1e81bad2d2ecb4f8b3b2b1b0f4;


public class Vehiculo
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SIGERSIV.Models.Vehiculo, SIGERSIV", Vehiculo.class, __md_methods);
	}


	public Vehiculo ()
	{
		super ();
		if (getClass () == Vehiculo.class)
			mono.android.TypeManager.Activate ("SIGERSIV.Models.Vehiculo, SIGERSIV", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

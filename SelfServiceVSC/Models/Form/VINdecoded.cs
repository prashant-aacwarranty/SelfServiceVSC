using System;
using System.Collections.Generic;

public class VehicleInfo
{
	public int Count { get; set; }
	public string Message { get; set; }
	public string SearchCriteria { get; set; }
	public List<VehicleResult> Results { get; set; }
}

public class VehicleResult
{
	public string Value { get; set; }
	public string ValueId { get; set; }
	public string Variable { get; set; }
	public int VariableId { get; set; }
}



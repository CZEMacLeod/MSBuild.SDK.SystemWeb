using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace WCF.Service.ClassLibrary;

[ServiceContract]
public interface ICalculator2
{
	[OperationContract]
	[WebGet]
	double Add(double n1, double n2);
	[OperationContract]
	[WebGet]
	double Subtract(double n1, double n2);
	[OperationContract]
	[WebGet]
	double Multiply(double n1, double n2);
	[OperationContract]
	[WebGet]
	double Divide(double n1, double n2);
}

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class CalculatorService2 : ICalculator2
{
	public double Add(double n1, double n2)
	{
		return n1 + n2;
	}
	public double Subtract(double n1, double n2)
	{
		return n1 - n2;
	}
	public double Multiply(double n1, double n2)
	{
		return n1 * n2;
	}
	public double Divide(double n1, double n2)
	{
		return n1 / n2;
	}
}
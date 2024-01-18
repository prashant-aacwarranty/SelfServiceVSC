using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AAC.Libraries;
using FastExpressionCompiler.LightExpression;

namespace AAC.SelfServiceVSC.Models.PaylinkAPI
{
	public enum CoverageType : UInt64
	{
		/// <summary>
		/// Vehicle Service Contract
		/// </summary>
		AUTO = 1L << 0,
		/// <summary>
		/// Boat
		/// </summary>
		BOAT = 1L << 1,
		/// <summary>
		/// Equipment
		/// </summary>
		EQUIPMENT = 1L << 2,
		/// <summary>
		/// Genesis Protection Plan - Gap & Gap Plus
		/// </summary>
		GEGP = 1L << 3,
		/// <summary>
		/// Genesis Protection Plan – Key Replacement
		/// </summary>
		GEKR = 1L << 4,
		/// <summary>
		/// Genesis Protection Plan - Lease End Protection
		/// </summary>
		GELS = 1L << 5,
		/// <summary>
		/// Genesis Protection Plan - Pre-Paid Maintenance
		/// </summary>
		GEPM = 1L << 6,
		/// <summary>
		/// Pre-Paid Scheduled Maintenance Plus
		/// </summary>
		GEPP = 1L << 7,
		/// <summary>
		/// Genesis Protection Plan - Pre-Paid Scheduled Maintenance Plus
		/// </summary>
		GESPPM = 1L << 8,
		/// <summary>
		/// Genesis Protection Plan - Term Protection Plus
		/// </summary>
		GETC = 1L << 9,
		/// <summary>
		/// Genesis Protection Plan – Tire & Wheel Protection
		/// </summary>
		GETW = 1L << 10,
		/// <summary>
		/// Genesis Protection Plan - Platinum Vehicle Protection
		/// </summary>
		GEUP = 1L << 11,
		/// <summary>
		/// Genesis Protection Plan – Livery VSC
		/// </summary>
		GEVB = 1L << 12,
		/// <summary>
		/// Genesis Protection Plan - Vehicle Service Contract
		/// </summary>
		GEVS = 1L << 13,
		/// <summary>
		/// Home Service Contract
		/// </summary>
		HOME = 1L << 14,
		/// <summary>
		/// Hyundai Protection Plan - Certification Contract
		/// </summary>
		HYCO = 1L << 15,
		/// <summary>
		/// Hyundai Protection Plan - CPO Wrap
		/// </summary>
		HYCP = 1L << 16,
		/// <summary>
		/// Hyundai Protection Plan - GAP and GAP Plus
		/// </summary>
		HYGP = 1L << 17,
		/// <summary>
		/// Hyundai Protection Plan - Key Replacement
		/// </summary>
		HYKR = 1L << 18,
		/// <summary>
		/// Hyundai Protection Plan - Excess Wear & Use
		/// </summary>
		HYLS = 1L << 19,
		/// <summary>
		/// Hyundai Protection Plan - Pre-Paid Maintenance
		/// </summary>
		HYPM = 1L << 20,
		/// <summary>
		/// Hyundai Protection Plan – Pre-Paid Scheduled Maintenance
		/// </summary>
		HYPMS = 1L << 21,
		/// <summary>
		/// Hyundai Protection Plan - Pre-Paid Maintenance w/TR
		/// </summary>
		HYPMT = 1L << 22,
		/// <summary>
		/// Hyundai Protection Plan - Term Protection Plus
		/// </summary>
		HYTC = 1L << 23,
		/// <summary>
		/// Hyundai Protection Plan – Tire & Wheel Protection
		/// </summary>
		HYTW = 1L << 24,
		/// <summary>
		/// Hyundai Protection Plan – Platinum Vehicle Protection
		/// </summary>
		HYUP = 1L << 25,
		/// <summary>
		/// Hyundai Protection Plan - Livery VSC
		/// </summary>
		HYVB = 1L << 26,
		/// <summary>
		/// Hyundai Protection Plan – Hyundai Circle VSC
		/// </summary>
		HYVE = 1L << 27,
		/// <summary>
		/// Hyundai Protection Plan - Original Owner VSC
		/// </summary>
		HYVO = 1L << 28,
		/// <summary>
		/// Hyundai Protection Plan - Vehicle Service Contract
		/// </summary>
		HYVS = 1L << 29,
		/// <summary>
		/// Lift
		/// </summary>
		LIFT = 1L << 30,
		/// <summary>
		/// Marine
		/// </summary>
		MAR = 1L << 31,
		/// <summary>
		/// Motorcycle
		/// </summary>
		ML = 1L << 32,
		/// <summary>
		/// Other (subtype: HEARING DEVICE)
		/// </summary>
		OTHER = 1L << 33,
		/// <summary>
		/// Powersports
		/// </summary>
		POW = 1L << 34,
		/// <summary>
		/// Power Protect Plan - CPO Wrap
		/// </summary>
		PPCP = 1L << 35,
		/// <summary>
		/// Power Protect Plan - GAP and GAP Plus
		/// </summary>
		PPGP = 1L << 36,
		/// <summary>
		/// Power Protect Plan - Key Replacement
		/// </summary>
		PPKR = 1L << 37,
		/// <summary>
		/// Power Protect Plan - Excess Wear & Use
		/// </summary>
		PPLS = 1L << 38,
		/// <summary>
		/// Pre-paid Maintenance
		/// </summary>
		PPM = 1L << 39,
		/// <summary>
		/// Power Protect Plan - Pre-Paid Maintenance
		/// </summary>
		PPPM = 1L << 40,
		/// <summary>
		/// PP – Pre-Paid Maintenance w/ TR
		/// </summary>
		PPPMT = 1L << 41,
		/// <summary>
		/// Power Protect Plan - Term Protection Plus
		/// </summary>
		PPTC = 1L << 42,
		/// <summary>
		/// PP – Tire & Wheel Protection
		/// </summary>
		PPTW = 1L << 43,
		/// <summary>
		/// Power Protect Plan - Platinum Vehicle Protection
		/// </summary>
		PPUP = 1L << 44,
		/// <summary>
		/// Power Protect Plan - Vehicle Service Contract
		/// </summary>
		PPVS = 1L << 45,
		/// <summary>
		/// Product
		/// </summary>
		PR = 1L << 46,
		/// <summary>
		/// RV
		/// </summary>
		RV = 1L << 47,
		/// <summary>
		/// Semi-Truck
		/// </summary>
		SEMI = 1L << 48,
		/// <summary>
		/// Anti-theft 
		/// </summary>
		THEFT = 1L << 49,
		/// <summary>
		/// Tire and wheel
		/// </summary>
		TIRE = 1L << 50,
		/// <summary>
		/// Truck
		/// </summary>
		TR = 1L << 51,
		/// <summary>
		/// Window & Dent
		/// </summary>
		WINDENT = 1L << 52
	}
}

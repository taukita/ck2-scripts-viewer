using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2ScriptsParser.SyntaxUnits;
using Ck2ScriptsParser.TreeModel;

namespace Ck2ScriptsParser.Ck2Objects
{
    /// <summary>
    /// http://www.ckiiwiki.com/Trait_modding
    /// </summary>
    public class Ck2Trait : Ck2BaseObject
    {
		//[Ck2Property("")]

		#region Type

		[Ck2Property("agnatic")]
        public bool? Agnatic
        {
            get;
            set;
        }

		[Ck2Property("birth")]
        public int? Birth
        {
            get;
            set;
        }

		[Ck2Property("blinding")]
        public bool? Blinding
        {
            get;
            set;
        }

		[Ck2Property("cached")]
        public bool? Cached
        {
            get;
            set;
        }

	    [Ck2Property("cannot_inherit")]
	    public bool? CannotInherit
	    {
		    get;
		    set;
	    }

	    [Ck2Property("cannot_marry")]
	    public bool? CannotMarry
	    {
		    get;
		    set;
	    }

	    [Ck2Property("caste_tier")]
	    public int? CasteTier
	    {
		    get;
		    set;
	    }

	    [Ck2Property("customizer")]
	    public bool? Customizer
	    {
		    get;
		    set;
	    }

	    [Ck2Property("education")]
	    public bool? Education
	    {
		    get;
		    set;
	    }

	    [Ck2Property("immortal")]
	    public bool? Immortal
	    {
		    get;
		    set;
	    }

	    [Ck2Property("in_hiding")]
	    public bool? InHiding
	    {
		    get;
		    set;
	    }

	    [Ck2Property("inbred")]
	    public bool? Inbred
	    {
		    get;
		    set;
	    }

	    [Ck2Property("incapacitating")]
	    public bool? Incapacitating
	    {
		    get;
		    set;
	    }

	    [Ck2Property("inherit_chance")]
	    public int? InheritChance
	    {
		    get;
		    set;
	    }

	    [Ck2Property("is_epidemic")]
	    public bool? IsEpidemic
	    {
		    get;
		    set;
	    }

	    [Ck2Property("is_health")]
	    public bool? IsHealth
	    {
		    get;
		    set;
	    }

	    [Ck2Property("is_illness")]
	    public bool? IsIllness
	    {
		    get;
		    set;
	    }

	    [Ck2Property("leader")]
	    public bool? Leader
	    {
		    get;
		    set;
	    }

	    [Ck2Property("leadership_traits")]
	    public int? LeadershipTraits
	    {
		    get;
		    set;
	    }

	    [Ck2Property("lifestyle")]
	    public bool? Lifestyle
	    {
		    get;
		    set;
	    }

	    [Ck2Property("opposites"), UseCode]
	    public List<Ck2Trait> Opposites
	    {
		    get;
		    set;
	    }

	    [Ck2Property("personality")]
	    public bool? Personality
	    {
		    get;
		    set;
	    }

	    [Ck2Property("prevent_decadence")]
	    public bool? PreventDecadence
	    {
		    get;
		    set;
	    }

	    [Ck2Property("priest")]
	    public bool? Priest
	    {
		    get;
		    set;
	    }

	    [Ck2Property("pilgrimage")]
	    public bool? Pilgrimage
	    {
		    get;
		    set;
	    }

	    [Ck2Property("random")]
	    public bool? Random
	    {
		    get;
		    set;
	    }

	    [Ck2Property("rebel_inherited")]
	    public bool? RebelInherited
	    {
		    get;
		    set;
	    }

	    [Ck2Property("religious")]
	    public bool? Religious
	    {
		    get;
		    set;
	    }

	    [Ck2Property("religious_branch"), UseCode]
	    public Ck2BaseObject ReligiousBranch //TODO Ck2Religion!
	    {
		    get;
		    set;
	    }

	    [Ck2Property("ruler_designer_cost")]
	    public int? RulerDesignerCost
	    {
		    get;
		    set;
	    }

	    [Ck2Property("tolerates_christian")]
	    public bool? ToleratesChristian
	    {
		    get;
		    set;
	    }

	    [Ck2Property("tolerates_jewish_group")]
	    public bool? ToleratesJewishGroup
	    {
		    get;
		    set;
	    }

	    [Ck2Property("tolerates_muslim")]
	    public bool? ToleratesMuslim
	    {
		    get;
		    set;
	    }

	    [Ck2Property("tolerates_pagan_group")]
	    public bool? ToleratesPaganGroup
	    {
		    get;
		    set;
	    }

	    [Ck2Property("tolerates_zoroastrian_group")]
	    public bool? ToleratesZoroastrianGroup
	    {
		    get;
		    set;
	    }

	    [Ck2Property("tolerates_indian_group")]
	    public bool? ToleratesIndianGroup
	    {
		    get;
		    set;
		}

		#endregion

		#region Attributes

	    [Ck2Property("command_modifier")]
	    public Ck2BaseObject CommandModifier //TODO Ck2CommandModifier
	    {
		    get;
		    set;
	    }

	    [Ck2Property("diplomacy")]
	    public int? Diplomacy
	    {
		    get;
		    set;
	    }

	    [Ck2Property("stewardship")]
	    public int? Stewardship
	    {
		    get;
		    set;
	    }

	    [Ck2Property("martial")]
	    public int? Martial
	    {
		    get;
		    set;
	    }

	    [Ck2Property("intrigue")]
	    public int? Intrigue
	    {
		    get;
		    set;
	    }

	    [Ck2Property("learning")]
	    public int? Learning
	    {
		    get;
		    set;
	    }

	    [Ck2Property("fertility")]
		public decimal? Fertility
	    {
		    get;
		    set;
	    }

	    [Ck2Property("health")]
	    public decimal? Health
	    {
		    get;
		    set;
	    }

		#endregion
	}
}

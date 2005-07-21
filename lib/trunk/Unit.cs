using System;
using System.Collections.Generic;

namespace Wc3o {
	[Serializable]
	public class Unit : Entity {

		#region " Constructor "
		public Unit() {
		}

		public Unit(UnitType t, Sector s, Player p, DateTime d) {
			this.type = t;
			this.sector = s;
			this.owner = p;
			this.date = d;
			this.action = UnitAction.None;
			this.hitpoints = Info.Hitpoints;
			this.number = 1;
			Sector.Units.Add(this);
			if (Owner != null)
				Owner.Units.Add(this);
		}
		#endregion

		#region " Create "
		public override void Create(Sector s) {
			if (!s.Units.Contains(this))
				s.Units.Add(this);
			if (Owner != null)
				Owner.Units.Add(this);
		}
		#endregion

		#region " Destroy "
		public override void Destroy() {
			Sector.Units.Remove(this);
			if (Owner != null)
				Owner.Units.Remove(this);
		}
		#endregion

		#region " Properties "
		public bool IsVisible {
			get {
				if (UnitInfo.Visibility == Visibility.Never)
					return false;
				else if (UnitInfo.Visibility == Visibility.AtNight && Game.IsNight)
					return false;
				else if (UnitInfo.Visibility == Visibility.AtDay && !Game.IsNight)
					return false;
				return true;
			}
		}

		public override bool IsAvailable {
			get {
				return !IsWorking && !IsInTraining && !IsMoving && !IsReturning;
			}
		}

		public bool IsWorking {
			get {
				return Action == UnitAction.WorkForGold || Action == UnitAction.WorkForLumber;
			}
		}

		public bool IsInTraining {
			get {
				return Date >= DateTime.Now && SourceSector == null;
			}
		}

		public bool IsMoving {
			get {
				return Date >= DateTime.Now && SourceSector != null && Action == UnitAction.Moving;
			}
		}

		public bool IsReturning {
			get {
				return Date >= DateTime.Now && SourceSector != null && Action == UnitAction.Returning;
			}
		}

		public override EntityInfo Info {
			get {
				return UnitInfo.Get(Type);
			}
		}

		public UnitInfo UnitInfo {
			get {
				return UnitInfo.Get(Type);
			}
		}

		UnitType type;
		public UnitType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}

		Player owner;
		public Player Owner {
			get {
				return owner;
			}
		}

		UnitAction action;
		public UnitAction Action {
			get {
				return action;
			}
			set {
				action = value;
			}
		}

		public new Sector Sector {
			get {
				return sector;
			}
			set {
				sector.Units.Remove(this);
				sector = value;
				sector.Units.Add(this);
			}
		}

		Sector sourceSector;
		public Sector SourceSector {
			get {
				return sourceSector;
			}
			set {
				sourceSector = value;
			}
		}

		DateTime sourceDate;
		public DateTime SourceDate {
			get {
				return sourceDate;
			}
			set {
				sourceDate = value;
			}
		}
		#endregion

		#region " Methods "
		public override Entity Clone() {
			Unit u = new Unit(Type, Sector, Owner, Date);
			u.Hitpoints = Hitpoints;
			u.Action = Action;
			u.SourceSector = SourceSector;
			u.SourceDate = SourceDate;
			u.Number = Number;
			u.Armor = Armor;
			u.AttackAir = AttackAir;
			u.AttackGround = AttackGround;
			u.Cooldown = Cooldown;
			u.Range = Range;
			return u;
		}
		#endregion

	}

	public enum UnitType {
		Abomination,
		Acolyte,
		Arachnathid,
		Archer,
		Bandit,
		BanditLord,
		Banshee,
		BattleGolem,
		BlueDragonspawnMeddler,
		Brigand,
		BroodMother,
		CentaurArcher,
		CentaurDrudge,
		CentaurKhan,
		Chimaera,
		CorruptedTreant,
		CryptFiend,
		DarkTroll,
		DarkTrollPriest,
		DarkTrollTrapper,
		DeeplordRevenant,
		Demolisher,
		Destroyer,
		DireWolf,
		DoomGuard,
		DraeneiGuardian,
		Dragon,
		DragonhawkRider,
		DruidOfTheClawBearForm,
		DruidOfTheClawDruidForm,
		DruidOfTheTalonCrowForm,
		DruidOfTheTalonDruidForm,
		Dryad,
		ElderJungleStalker,
		EnragedElemental,
		EredarSorceror,
		FacelessOneTrickster,
		FaerieDragon,
		FallenPriest,
		FelBeast,
		Felguard,
		FlyingMachine,
		Footman,
		ForestTroll,
		ForestTrollShadowPriest,
		ForestTrollTrapper,
		FrostRevenant,
		FrostWolf,
		FrostWyrm,
		Furbolg,
		FurbolgShaman,
		FurbolgTracker,
		FurbolgUrsaWarrior,
		Gargoyle,
		Ghost,
		Ghoul,
		GiantSkeletonWarrior,
		GlaiveThrower,
		Gnoll,
		GnollOverseer,
		GnollWarden,
		Grunt,
		GryphonRider,
		HarpyQueen,
		HarpyRogue,
		HarpyWindwitch,
		Hippogryph,
		HippogryphRider,
		Huntress,
		Hydra,
		IceTroll,
		IceTrollPriest,
		InfernalJuggernaut,
		Knight,
		Kobold,
		KoboldGeomancer,
		KodoBeast,
		MagnataurWarrior,
		MakruraPooldweller,
		Mammoth,
		MeatWagon,
		Militia,
		MortarTeam,
		MountainGiant,
		MurgulCliffrunner,
		MurlocFlesheater,
		MurlocHuntsman,
		MurlocMutant,
		MurlocNightcrawler,
		MurlocTiderunner,
		Necromancer,
		NerubianQueen,
		NerubianWarrior,
		NerubianWebspinner,
		NetherDrake,
		None,
		ObsidianStatue,
		OgreLord,
		OgreMagi,
		OgreWarrior,
		Pandaren,
		Peasant,
		Peon,
		PolarBear,
		PolarFurbolg,
		PolarFurbolgShaman,
		PolarFurbolgTracker,
		PolarFurbolgUrsaWarrior,
		Priest,
		QueenOfSuffering,
		Quillboar,
		Raider,
		RazormaneChieftain,
		RazormaneScout,
		Rifleman,
		RockGolem,
		RogueWizard,
		Salamander,
		Sasquatch,
		Satyr,
		SatyrTrickster,
		SeaElemental,
		SeaGiant,
		SeaTurtle,
		Shade,
		Shaman,
		SiegeEngine,
		SkeletalMage,
		SkeletalOrcGrunt,
		SkeletonArcher,
		SkeletonWarrior,
		SludgeMinion,
		Sorceress,
		SpellBreaker,
		Spider,
		SpiderCrabBehemoth,
		SpiritWalker,
		SpittingSpider,
		StormreaverWarlock,
		Tauren,
		ThunderLizard,
		TimberWolf,
		TrollBatrider,
		TrollBerserker,
		TrollHeadhunter,
		TuskarrFighter,
		UnbrokenDarkhunter,
		Voidwalker,
		Wendigo,
		WendigoShaman,
		Wildkin,
		WindRider,
		Wisp,
		WitchDoctor
	}
	public enum UnitAction {
		None,
		Moving,
		Returning,
		WorkForGold,
		WorkForLumber
	}

}

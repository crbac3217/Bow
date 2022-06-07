using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public ItemDatabase itemdb;
    public SetDataBase setdb;
    public EquipmentDataBase eqdb;
    public PlayerControl pc;

    public void SetPlayer()
    {
        itemdb = Instantiate(pc.playerType.itemdb);
        setdb = Instantiate(pc.playerType.setdb);
        eqdb = Instantiate(pc.playerType.equipdb);
        }
    public void AddItem(string itemcode)
    {
        foreach (Item item in itemdb.items)
        {
            if (item.nameCode == itemcode)
            {
                foreach (Item pitem in pc.items)
                {
                    if (pitem.nameCode == itemcode)
                    {
                        AddExistingItem(item, pitem);
                        return;
                    }
                }
                AddNewItem(item);
            }
        }
    }
    public void AddNewItem(Item item)
    {
        pc.items.Add(item);
        foreach (Stat stat in item.statMods)
        {
            StatAdd(stat);
        }
        foreach (DamageType dType in item.damageMods)
        {
            DamageAdd(dType);
        }
        foreach (Modifier mod in item.modifiers)
        {
            AddMod(mod);
        }
        CheckSet(item.nameCode);
        pc.ReloadStats();
    }
    public void AddExistingItem(Item item, Item pitem)
    {
        pitem.multi++;
        foreach (Stat stat in item.statMods)
        {
            StatAdd(stat);
        }
        foreach (DamageType dType in item.damageMods)
        {
            DamageAdd(dType);
        }
        foreach (Modifier mod in item.modifiers)
        {
            AddMod(mod);
        }
        pc.ReloadStats();
    }
    public void AddEquip(string eqname)
    {
        foreach (Equipment eq in eqdb.equipments)
        {
            if (eq.name == eqname)
            {
                ReplaceEquipment(eq);
            }
        }
        pc.ReloadStats();
    }
    public void ReplaceEquipment(Equipment eq)
    {
        Equipment prevEq = null;
        foreach (Equipment temp in pc.equipments)
        {
            if (temp.eType == eq.eType)
            {
                prevEq = temp;
            }
        }
        if (prevEq != null)
        {
            RemovePrevEq(prevEq);
        }
        AddNewEq(eq);
        CheckSet(eq.nameCode);
    }
    public void RemovePrevEq(Equipment eq)
    {
        pc.equipments.Remove(eq);
        foreach (Stat stat in eq.statMods)
        {
            StatRemove(stat);
        }
        foreach (DamageType dType in eq.damageMods)
        {
            DamageRemove(dType);
        }
        foreach (Modifier mod in eq.modifiers)
        {
            RemoveMod(mod);
        }
        //foreach (ScriptFunction sfunc in eq.scripts)
        //{
        //    DeactivateScriptFunction(sfunc);
        //}
        pc.RemoveSkill(eq);
        
    }
    public void AddNewEq(Equipment eq)
    {
        pc.equipments.Add(eq);
        foreach (Stat stat in eq.statMods)
        {
            StatAdd(stat);
        }
        foreach (DamageType dType in eq.damageMods)
        {
            DamageAdd(dType);
        }
        foreach (Modifier mod in eq.modifiers)
        {
            AddMod(mod);
        }
        //foreach (ScriptFunction sfunc in eq.scripts)
        //{
        //    ActivateScriptFunction(sfunc);
        //}
        pc.AddSkill(eq);
    }
    public void ArrangeEquips()
    {
        List<Equipment> temporary = new List<Equipment>();
        foreach (Equipment equipment in pc.equipments)
        {
            temporary.Add(equipment);
        }
        pc.equipments.Clear();
        foreach (Equipment eq in pc.equipments)
        {
            for (int i = 0; i < Enum.GetNames(typeof(Equipment.EquipmentType)).Length; i++)
            {
                if (eq.eType == (Equipment.EquipmentType)i)
                {
                    pc.equipments.Add(eq);
                }
            }
        }
    }
    public void CheckSet(string namecode)
    {
        foreach (Set set in setdb.sets)
        {
            if (set.setCodes.Contains(namecode))
            {
                if (!pc.sets.Contains(set))
                {
                    pc.sets.Add(set);
                }
                if (!set.collected.Contains(namecode))
                {
                    set.collected.Add(namecode);
                    set.currentCount = set.collected.Count;
                }
            }
        }
        CheckSetEffect();
    }
    public void CheckSetEffect()
    {
        foreach (Set set in pc.sets)
        {
            foreach (SetEffect setEffect in set.setEffects)
            {
                if (set.collected.Count >= setEffect.setCount && !setEffect.currentlyActive)
                {
                    setEffect.currentlyActive = true;
                    ActivateSetEffect(setEffect);
                }
            }
        }
    }
    public void ActivateSetEffect(SetEffect effect)
    {
        foreach (Modifier mod in effect.modifiers)
        {
            AddMod(mod);
        }
        foreach (Stat stat in effect.statMods)
        {
            StatAdd(stat);
        }
        foreach(DamageType dtype in effect.damageMods)
        {
            DamageAdd(dtype);
        }
        pc.guiManager.DisplaySetText(effect);
    }
    public void StatAdd(Stat stat)
    {
        foreach (Stat pstat in pc.stats)
        {
            if (stat.statType == pstat.statType)
            {
                pstat.value += stat.value;
                pstat.value = (int)MathF.Max(pstat.value, 0);
                if (stat.statType == StatType.hp)
                {
                    if (stat.value > 0)
                    {
                        pc.currentHp += stat.value;
                    }
                    pstat.value = (int)MathF.Max(pstat.value, 1);
                }
            }
        }
    }
    public void StatRemove(Stat stat)
    {
        foreach (Stat pstat in pc.stats)
        {
            if (stat.statType == pstat.statType)
            {
                pstat.value -= stat.value;
            }
        }
    }
    public void DamageAdd(DamageType dType)
    {
        foreach (DamageType pDtype in pc.damageTypes)
        {
            if (dType.damageElement == pDtype.damageElement)
            {
                pDtype.value += dType.value;
                return;
            }
        }
        DamageType dt = new DamageType
        {
            damageElement = dType.damageElement,
            value = dType.value
        };
        pc.damageTypes.Add(dt);
    }
    public void DamageRemove(DamageType dType)
    {
        foreach (DamageType pDtype in pc.damageTypes)
        {
            if (dType.damageElement == pDtype.damageElement)
            {
                pDtype.value -= dType.value;
            }
        }
    }
    public void AddMod(Modifier mod)
    {
        Modifier temp = Instantiate(mod);
        if (temp.modifierType == ModifierType.OnAdd)
        {
            pc.onAddList.Add(temp);
            if (temp.onAdd)
            {
                temp.OnModifierAdd(pc);
            }
        }
            else if (temp.modifierType == ModifierType.AnyAttack)
        {
            pc.ps.shootMods.Add(temp);
            if (temp.onAdd)
            {
                temp.OnModifierAdd(pc);
            }
        }
            else if (temp.modifierType == ModifierType.Hit)
        {
            pc.ph.hitList.Add(temp);
            if (temp.onAdd)
            {
                temp.OnModifierAdd(pc);
            }
        }
        else if (temp.modifierType == ModifierType.Dead)
        {
            pc.ph.deadList.Add(temp);
            if (temp.onAdd)
            {
                temp.OnModifierAdd(pc);
            }
        }
        else if (temp.modifierType == ModifierType.AttackHit)
        {
            pc.damageManager.damagedMods.Add(temp);
            if (temp.onAdd)
            {
                temp.OnModifierAdd(pc);
            }
        }
        else if (temp.modifierType == ModifierType.Killed)
        {
            pc.damageManager.killedMods.Add(temp);
            if (temp.onAdd)
            {
                temp.OnModifierAdd(pc);
            }
        }
        else if (temp.modifierType == ModifierType.Move)
        {
            pc.pm.moveMods.Add(temp);
            if (temp.onAdd)
            {
                temp.OnModifierAdd(pc);
            } 
        }
    }
    public void RemoveMod(Modifier mod)
    {
        if (mod.modifierType == ModifierType.OnAdd)
        {
            for (int i = pc.onAddList.Count - 1; i > -1 ; i--)
            {
                if (pc.onAddList[i].GetType() == mod.GetType())
                {
                    pc.onAddList[i].OnModifierRemove(pc);
                    pc.onAddList.Remove(pc.onAddList[i]);
                }
            }
        }
        else if (mod.modifierType == ModifierType.AnyAttack)
        {
            for (int i = pc.ps.shootMods.Count - 1; i > -1; i--)
            {
                if (pc.ps.shootMods[i].GetType() == mod.GetType())
                {
                    pc.onAddList[i].OnModifierRemove(pc);
                    pc.ps.shootMods.Remove(pc.ps.shootMods[i]);
                }
            }
        }
        else if (mod.modifierType == ModifierType.Hit)
        {
            for (int i = pc.ph.hitList.Count - 1; i > -1; i--)
            {
                if (pc.ph.hitList[i].GetType() == mod.GetType())
                {
                    pc.onAddList[i].OnModifierRemove(pc);
                    pc.ph.hitList.Remove(pc.ph.hitList[i]);
                }
            }
        }
        else if (mod.modifierType == ModifierType.Dead)
        {
            for (int i = pc.ph.deadList.Count - 1; i > -1; i--)
            {
                if (pc.ph.deadList[i].GetType() == mod.GetType())
                {
                    pc.onAddList[i].OnModifierRemove(pc);
                    pc.ph.deadList.Remove(pc.ph.deadList[i]);
                }
            }
        }
        else if (mod.modifierType == ModifierType.AttackHit)
        {
            for (int i = pc.damageManager.damagedMods.Count - 1; i > -1; i--)
            {
                if (pc.damageManager.damagedMods[i].GetType() == mod.GetType())
                {
                    pc.onAddList[i].OnModifierRemove(pc);
                    pc.damageManager.damagedMods.Remove(pc.damageManager.damagedMods[i]);
                }
            }
        }
        else if (mod.modifierType == ModifierType.Killed)
        {
            for (int i = pc.damageManager.killedMods.Count - 1; i > -1; i--)
            {
                if (pc.damageManager.killedMods[i].GetType() == mod.GetType())
                {
                    pc.onAddList[i].OnModifierRemove(pc);
                    pc.damageManager.killedMods.Remove(pc.damageManager.killedMods[i]);
                }
            }
        }
        else if (mod.modifierType == ModifierType.Move)
        {
            for (int i = pc.pm.moveMods.Count - 1; i > -1; i--)
            {
                if (pc.pm.moveMods[i].GetType() == mod.GetType())
                {
                    pc.onAddList[i].OnModifierRemove(pc);
                    pc.pm.moveMods.Remove(pc.pm.moveMods[i]);
                }
            }
        }
    }
}

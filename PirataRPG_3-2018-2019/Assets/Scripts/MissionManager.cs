using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Assets.Scripts.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    List<Mission> loadedMissions = new List<Mission>();
    Mission newMission;
    MissionTask newMissionTask;
    TaskCondition newTaskCondition;
    TaskAction newTaskAction;
    List<MissionTask> tasksRemoved = new List<MissionTask>();
    List<Mission> missionsRemoved = new List<Mission>();
    private const float MAXCLOSEDISTANCE = 1f;

    private void Update()
    {
        //Lets check missions
        foreach (Mission currentMission in GetNextMissions())
        {
            foreach (MissionTask currenTask in currentMission.MissionTasks)
            {
                if (ConditionsMet(currenTask))
                {
                    ExecuteTaskActions(currenTask);
                    tasksRemoved.Add(currenTask);
                }
            }

            foreach (MissionTask currrenTask in tasksRemoved)
            {
                currentMission.MissionTasks.Remove(currrenTask);
                if(currentMission.MissionTasks.Count == 0)
                    missionsRemoved.Add(currentMission);
            }
        }

        foreach (Mission currentMission in missionsRemoved)
        {
            Game.Instance().CurrentLevel.Missions.Remove(currentMission);
        }
    }

    public void LoadMissions(XmlDocument xmlDoc)
    {
        var selectedNodes =
            xmlDoc.SelectNodes("//level/missions/mission");

        foreach (XmlNode currentNode in selectedNodes)
        {
            newMission = new Mission
            {
                id = currentNode.Attributes["id"].Value,
                description = currentNode.Attributes["description"].Value,
                prerequisites = currentNode.Attributes["prerequisites"].Value
            };

            newMission.MissionTasks = new List<MissionTask>();


            var selectedTask =
                xmlDoc.SelectNodes(string.Format("//level/missions/mission[@id='{0}']/tasks/task", newMission.id));

            foreach (XmlNode currentTask in selectedTask)
            {
                newMissionTask = new MissionTask
                {
                    id = currentTask.Attributes["id"].Value,
                    description = currentTask.Attributes["description"].Value

                };
                newMissionTask.TaskConditions = new List<TaskCondition>();

                var selectedTaskConditions = xmlDoc.SelectNodes(string.Format("//level/missions/mission[@id='{0}']/tasks/task[@id='{1}']/conditions/condition", newMission.id, newMissionTask.id));
                foreach (XmlNode currentTaskCondition in selectedTaskConditions)
                {
                    newTaskCondition = new TaskCondition
                    {
                        Type = (TaskCondition.TaskConditionType)Enum.Parse(typeof(TaskCondition.TaskConditionType),
                            currentTaskCondition.Attributes["type"].Value),
                        uniqueObjectNameFrom = currentTaskCondition.Attributes["uniqueObjectNameFrom"].Value,
                        uniqueObjectNameTo = currentTaskCondition.Attributes["uniqueObjectNameTo"].Value
                    };
                    newMissionTask.TaskConditions.Add(newTaskCondition);
                }

                newMissionTask.TaskActions = new List<TaskAction>();

                var selectedTaskActions = xmlDoc.SelectNodes(string.Format("//level/missions/mission[@id='{0}']/tasks/task[@id='{1}']/actions/action", newMission.id, newMissionTask.id));
                foreach (XmlNode currentTaskCondition in selectedTaskActions)
                {
                    newTaskAction = new TaskAction
                    {
                        Type = (TaskAction.TaskActionType)Enum.Parse(typeof(TaskAction.TaskActionType),
                            currentTaskCondition.Attributes["type"].Value),
                        uniqueObjectNameFrom = currentTaskCondition.Attributes["uniqueObjectNameFrom"].Value,
                        uniqueObjectNameTo = currentTaskCondition.Attributes["uniqueObjectNameTo"].Value,
                        Quantity = Convert.ToSingle(currentTaskCondition.Attributes["quantity"].Value)
                    };

                    newMissionTask.TaskActions.Add(newTaskAction);
                }
                newMission.MissionTasks.Add(newMissionTask);
            }

            Game.Instance().CurrentLevel.Missions.Add(newMission);
        }
    }

    List<Mission> GetNextMissions()
    {
        return loadedMissions.Where(m => string.IsNullOrEmpty(m.prerequisites)).ToList();
    }

    void CheckPrerrequisites(string missionId)
    {
        foreach (Mission currentMission in loadedMissions.Where(m => m.prerequisites.Split(',').Contains(missionId)))
        {
            List<string> newPrerrequisites = currentMission.prerequisites.Split(',').ToList();
            newPrerrequisites.Remove(missionId);
            currentMission.prerequisites = JoinPrerequisites(newPrerrequisites);
        }
    }

    string JoinPrerequisites(List<string> prerequisites)
    {
        string str = string.Empty;

        foreach (string currentPrerequisite in prerequisites)
        {
            str += currentPrerequisite + ",";
        }

        str.Remove(str.Length - 1);
        return str;
    }

    bool ConditionsMet(MissionTask currenTask)
    {
        foreach (TaskCondition currentCondition in currenTask.TaskConditions)
        {
            switch (currentCondition.Type)
            {
                case TaskCondition.TaskConditionType.CloseTo:
                    if (!IsCloseTo(
                    
                        Game.Instance().CurrentLevel.Entities.FirstOrDefault(ent => ent.UniqueObjectName == 
                         currentCondition.uniqueObjectNameFrom).gameObject,
                        Game.Instance().CurrentLevel.Entities.FirstOrDefault(ent => ent.UniqueObjectName ==
                         currentCondition.uniqueObjectNameTo).gameObject))
                         return false;
                    
   
                    break;
                case TaskCondition.TaskConditionType.Destroyed:
                    if (!IsDestroyed(currentCondition.uniqueObjectNameFrom))
                    {
                        return false;
                    }
                    break;
                case TaskCondition.TaskConditionType.Inventoried:
                    if (!IsInventoried(currentCondition.uniqueObjectNameFrom, currentCondition.Quantity))
                    {
                        
                    }
                    break;
                case TaskCondition.TaskConditionType.KeyPressed:
                    if (Input.GetButton(currentCondition.uniqueObjectNameFrom))
                    {
                        return false;
                    }
                    break;
            }
        }

        return true;
    }

    bool IsCloseTo(GameObject from, GameObject to)
    {
        return Vector3.Distance(from.transform.position, to.transform.position) <= MAXCLOSEDISTANCE;
    }

    bool IsDestroyed(string uniqueObjectName)
    {
        return !Game.Instance().CurrentLevel.Entities.Any(ent => ent.UniqueObjectName == uniqueObjectName);
    }

    bool IsInventoried(string prefabName, float quantity)
    {
        return Game.Instance().Inventory.Any(item => item.PrefabName == prefabName && item.Quantity >= quantity);
    }

    void ExecuteTaskActions(MissionTask currenTask)
    {
        foreach (TaskAction currentaAction in currenTask.TaskActions)
        {
            switch (currentaAction.Type)
            {
                case TaskAction.TaskActionType.InventoryAdd:
                    AddInventory(currentaAction.uniqueObjectNameFrom, currentaAction.Quantity);
                    break;
                case TaskAction.TaskActionType.LoadScene:
                    SceneManager.LoadScene(currentaAction.uniqueObjectNameFrom);
                    break;
                case TaskAction.TaskActionType.ShowMessage:
                    Debug.Log(currentaAction.uniqueObjectNameFrom);
                    break;
            }
        }
    }

    void AddInventory(string prefabName, float quantity)
    {
        InventoryItem item = Game.Instance().Inventory.Find(it => it.PrefabName == prefabName);

        if (item == null)
        {
            Game.Instance().Inventory.Add(new InventoryItem { Name = prefabName, PrefabName = prefabName, Quantity = quantity });
        }
        else
        {
            item.Quantity += quantity;
        }
    }
}

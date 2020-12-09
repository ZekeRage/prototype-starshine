﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    #region Inspector Attributes
    [SerializeField] Hazard[] hazardPrefabs;
    [SerializeField] bool VerboseConsole = true;
    #endregion

    #region Private Fields    
    private GridManager gm;

    private int currentTick = 0;
    private int ticksUntilNewSpawn;
    private int minTicksUntilSpawn = 2;
    private int maxTicksUntilSpawn = 4;
    
    private List<GridBlock> spawnMoveUp = new List<GridBlock>();
    private List<GridBlock> spawnMoveDown = new List<GridBlock>();
    private List<GridBlock> spawnMoveLeft = new List<GridBlock>();
    private List<GridBlock> spawnMoveRight = new List<GridBlock>();
    //private List<GridBlock> spawnMultiMove = new List<GridBlock>();

    Vector2Int minVector2;
    Vector2Int maxVector2;
    #endregion

    private List<Hazard> hazardsInPlay = new List<Hazard>();
    


    private void Start()
    {
        gm = GetComponent<GridManager>(); 
        minVector2 = new Vector2Int(gm.BoundaryLeftActual, gm.BoundaryBottomActual);
        maxVector2 = new Vector2Int(gm.BoundaryRightActual, gm.BoundaryTopActual);

        ticksUntilNewSpawn = Random.Range(minTicksUntilSpawn, maxTicksUntilSpawn);
    }

    public enum HazardType
    {
        SmallAsteroid = 1,
        LargeAsteroid = 2
    }

    public void Init()
    {
        UpdateSpawnLocations();
        CreateHazard();
        currentTick = 1;
    }

    /*
    private void UpdateSpawnLocations()
    {
        // IDEA
        // Generate a List of GridBlocks that hold GridBlocks eligible for spawning GridBlocksCanSpawn
        // Generate a List of GridBlocks that hold GridBlocks ineligible for spawning
        // Remove GridBlocks from the eligible List that exist in the ineligible List

        /*  SUMMARY
         *  - Set GridBlock.canSpawn on the opposite boundary of a hazard = false
         *  - Set GridBlock.canSpawn on a cell forward and to the left = false if the hazard is on a play boundary
         *  - Populate appropriate spawn List<GridBlock> based on grid location
         *

        if (VerboseConsole) Debug.Log("HazardManager.UpdateSpawnLocations called.");

        if (hazardsInPlay.Count > 0)
        {
            for(int i = 0; i < hazardsInPlay.Count; i++)
            {
                MovePattern hazardMove = hazardsInPlay[i].GetComponent<MovePattern>();
                
                Vector2Int hazardGridPosition = gm.WorldToGrid(hazardsInPlay[i].currentWorldLocation);

                if (hazardMove.delta == Vector2Int.up || hazardMove.delta == Vector2Int.down)
                {
                    int boundaryY;
                    if (hazardMove.delta == Vector2Int.up) boundaryY = gm.BoundaryTopActual;
                    else boundaryY = gm.BoundaryBottomActual;
                    
                    // Disable spawning on opposing GridBlock at boundary
                    gm.DeactivateGridBlockSpawn(new Vector2Int(hazardGridPosition.x, boundaryY));
                    
                    // Disable neighboring GridBlocks along immediate hazard trajectory
                    if (hazardGridPosition.x == gm.BoundaryLeftPlay) 
                    {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.left);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.left);

                    }
                    else if (hazardGridPosition.x == gm.BoundaryRightPlay) 
                        {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.right);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.right);
                    }

                }
                else if (hazardMove.delta == Vector2Int.left || hazardMove.delta == Vector2Int.right)
                {
                    // Disable spawning on opposing GridBlock at boundary
                    int boundaryX;
                    if (hazardMove.delta == Vector2Int.right) boundaryX = gm.BoundaryRightActual;   //colRange;
                    else boundaryX = gm.BoundaryLeftActual;
                    
                    gm.DeactivateGridBlockSpawn(new Vector2Int(boundaryX, hazardGridPosition.y));

                    // Disable neighboring GridBlocks along immediate hazard trajectory
                    if (hazardGridPosition.y == gm.BoundaryBottomPlay) {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.down);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.down);

                    }
                    else if (hazardGridPosition.y == gm.BoundaryTopPlay) {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.up);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.up);
                    }
                }
            }
            if (VerboseConsole) Debug.Log("HazardManager.UpdateSpawnLocations complete.");
        }

        // Populate spawn lists
        for (int i = 0; i < gm.levelGrid.GetLength(0); i++)
        {
            for (int j = 0; j < gm.levelGrid.GetLength(1); j++)
            {
                if (gm.levelGrid[i, j].canSpawn)
                {
                    Vector2Int testGridLocation = gm.levelGrid[i, j].location;

                    if (testGridLocation.x == gm.BoundaryLeftActual)
                        spawnMoveRight.Add(gm.levelGrid[i, j]);
                    else if (testGridLocation.x == gm.BoundaryRightActual)
                        spawnMoveLeft.Add(gm.levelGrid[i, j]);
                    else if (testGridLocation.y == gm.BoundaryTopActual)
                        spawnMoveDown.Add(gm.levelGrid[i, j]);
                    else
                        spawnMoveUp.Add(gm.levelGrid[i, j]);
                }
            }
        }

        Debug.Log("Spawn Locations successfully updated.");
    }
*/

    private void UpdateSpawnLocations()
    {
        if (VerboseConsole) Debug.Log("HazardManager.UpdateSpawnLocations called.");

        if (hazardsInPlay.Count > 0)
        {
            for (int i = 0; i < hazardsInPlay.Count; i++)
            {
                MovePattern hazardMove = hazardsInPlay[i].GetComponent<MovePattern>();
                Vector2Int hazardGridPosition = gm.WorldToGrid(hazardsInPlay[i].currentWorldLocation);

                if(hazardsInPlay[i].spawnRules.spawnRegion == GridManager.SpawnRule.SpawnRegion.Perimeter)
                {
                    //#RESUME
                }









                if (hazardMove.delta == Vector2Int.up || hazardMove.delta == Vector2Int.down)
                {
                    int boundaryY;
                    if (hazardMove.delta == Vector2Int.up) boundaryY = gm.BoundaryTopActual;
                    else boundaryY = gm.BoundaryBottomActual;

                    // Disable spawning on opposing GridBlock at boundary
                    gm.DeactivateGridBlockSpawn(new Vector2Int(hazardGridPosition.x, boundaryY));

                    // Disable neighboring GridBlocks along immediate hazard trajectory
                    if (hazardGridPosition.x == gm.BoundaryLeftPlay)
                    {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.left);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.left);

                    }
                    else if (hazardGridPosition.x == gm.BoundaryRightPlay)
                    {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.right);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.right);
                    }

                }
                else if (hazardMove.delta == Vector2Int.left || hazardMove.delta == Vector2Int.right)
                {
                    // Disable spawning on opposing GridBlock at boundary
                    int boundaryX;
                    if (hazardMove.delta == Vector2Int.right) boundaryX = gm.BoundaryRightActual;   //colRange;
                    else boundaryX = gm.BoundaryLeftActual;

                    gm.DeactivateGridBlockSpawn(new Vector2Int(boundaryX, hazardGridPosition.y));

                    // Disable neighboring GridBlocks along immediate hazard trajectory
                    if (hazardGridPosition.y == gm.BoundaryBottomPlay)
                    {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.down);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.down);

                    }
                    else if (hazardGridPosition.y == gm.BoundaryTopPlay)
                    {
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + Vector2Int.up);
                        gm.DeactivateGridBlockSpawn(hazardGridPosition + hazardMove.delta + Vector2Int.up);
                    }
                }
            }
            if (VerboseConsole) Debug.Log("HazardManager.UpdateSpawnLocations complete.");
        }

        // Populate spawn lists
        for (int i = 0; i < gm.levelGrid.GetLength(0); i++)
        {
            for (int j = 0; j < gm.levelGrid.GetLength(1); j++)
            {
                if (gm.levelGrid[i, j].canSpawn)
                {
                    Vector2Int testGridLocation = gm.levelGrid[i, j].location;

                    if (testGridLocation.x == gm.BoundaryLeftActual)
                        spawnMoveRight.Add(gm.levelGrid[i, j]);
                    else if (testGridLocation.x == gm.BoundaryRightActual)
                        spawnMoveLeft.Add(gm.levelGrid[i, j]);
                    else if (testGridLocation.y == gm.BoundaryTopActual)
                        spawnMoveDown.Add(gm.levelGrid[i, j]);
                    else
                        spawnMoveUp.Add(gm.levelGrid[i, j]);
                }
            }
        }

        Debug.Log("Spawn Locations successfully updated.");
    }

    private void CreateHazard()
    {
        /*  SUMMARY
        *   - Randomly select a spawn location
        *   - Randomly select a hazard to spawn
        *   - Activate hazard spawn movement pattern based on spawn location
        */
        if (VerboseConsole) Debug.Log("HazardManager.PrepareHazard() called.");

        int hazardType = Random.Range(0, hazardPrefabs.Length);
        if (VerboseConsole) Debug.LogFormat("Array Length: {0}, Random value: {1}", hazardPrefabs.Length, hazardType);

        int spawnAxis = Random.Range(1, 4);
        if (VerboseConsole) Debug.LogFormat("\n Randomly selected Spawn Axis = {0}.\n 1=Up \n 2=Down \n 3=Left \n 4=Right \n ", spawnAxis);
        
        int spawnIndex;
        Vector2Int spawnPosition = new Vector2Int();

        // Spawn hazard & store component references
        Hazard hazardToSpawn = Instantiate(hazardPrefabs[hazardType]);

        //Debug line
        //Hazard hazardToSpawn = Instantiate(hazardPrefabs[1]);
       
        hazardToSpawn.SetHazardAnimationMode(Hazard.HazardMode.Spawn);
        hazardToSpawn.GetComponent<Health>().ToggleInvincibility(true);

        MovePattern spawnMovement = hazardToSpawn.GetComponent<MovePattern>();
        Rotator spawnRotator = hazardToSpawn.GetComponent<Rotator>();

        switch (spawnAxis)
        {
            case 1:
                spawnIndex = Random.Range(0, spawnMoveUp.Count);
                spawnPosition = spawnMoveUp[spawnIndex].location;
                spawnMovement.SetMovePatternUp();
                spawnRotator.RotateUp();
                break;

            case 2:
                spawnIndex = Random.Range(0, spawnMoveDown.Count);
                spawnPosition = spawnMoveDown[spawnIndex].location;
                spawnMovement.SetMovePatternDown();
                spawnRotator.RotateDown();
                break;

            case 3:
                spawnIndex = Random.Range(0, spawnMoveLeft.Count);
                spawnPosition = spawnMoveLeft[spawnIndex].location;
                spawnMovement.SetMovePatternLeft();
                spawnRotator.RotateLeft();
                break;

            case 4:
                spawnIndex = Random.Range(0, spawnMoveRight.Count);
                spawnPosition = spawnMoveRight[spawnIndex].location;
                spawnMovement.SetMovePatternRight();
                spawnRotator.RotateRight();
                break;
        }

        AddHazard(hazardToSpawn, spawnPosition);

        if (VerboseConsole) Debug.Log("HazardManager.PrepareHazard() completed.");
    }


    public void AddHazard(Hazard hazard, Vector2Int gridLocation, bool placeOnGrid = true)
    {
        Debug.Log("HazardManager.AddHazard() called.");

        //GridBlock destinationGridPosition = gm.FindGridBlockByLocation(gridLocation);
        Vector3 worldLocation = gm.GridToWorld(gridLocation);

        //if(destinationGridPosition.IsAvailableForPlayer && placeOnGrid == false)
        if(placeOnGrid == false)
        {
            hazard.currentWorldLocation = worldLocation;
            hazard.targetWorldLocation = worldLocation;            

            hazardsInPlay.Add(hazard);
        }
        //else if(!destinationGridPosition.IsAvailableForPlayer)
        else
        {
            hazard.transform.position = worldLocation;
            gm.AddObjectToGrid(hazard.gameObject, gridLocation);

            hazard.currentWorldLocation = worldLocation;
            hazard.targetWorldLocation = worldLocation;

            hazardsInPlay.Add(hazard);
        }
    }

    public void RemoveHazardFromPlay(Hazard hazard)
    {
        GameObject hazardToRemove = hazard.gameObject;
        Vector2Int gridPosition = gm.FindGridBlockContainingObject(hazardToRemove).location;
        
        gm.RemoveObjectFromGrid(hazardToRemove, gridPosition);
        hazardsInPlay.Remove(hazard);
    }


    private bool CheckHazardHasHealth(GameObject hazardObject)
    {
        // This might better be suited as a Property in Health.cs

        Health hazardHealth = hazardObject.GetComponent<Health>();
        if (hazardHealth != null && hazardHealth.CurrentHP > 0)
        {
            return true;
        }
        return false;
    }

    private void HazardDropLoot(Hazard hazard)
    {
        LootHandler lh = hazard.gameObject.GetComponent<LootHandler>();
        if (lh != null)
        {
            GameObject lootObject = lh.RequestLootDrop(hazard.currentWorldLocation, forced: true);

            if (lootObject != null)
            {
                Vector2Int dropGridLocation = gm.WorldToGrid(hazard.currentWorldLocation);
                gm.AddObjectToGrid(lootObject, dropGridLocation);
                lootObject.GetComponent<Rotator>().enabled = true;
            }
        }
    }

    public float OnTickUpdate()
    {
        /*  STEPS
         * 
         *  1) Hazard Health Check
         *  2) Move hazards
         *  3) Detect Fly-Bys
         *  4) Hazard Health Check
         */

        #region Hazard Tick Duration
        bool moveOccurredThisTick = false;
        float moveDurationSeconds = 1.0f;

        bool hazardDestroyedThisTick = false;
        float destroyDurationSeconds = 2.0f;

        float delayTime = 0.0f;
        #endregion

        // Hazard Health Check - Check for hazards destroyed during Player turn
        for (int i = hazardsInPlay.Count - 1; i > -1; i--)
        {
            if (!CheckHazardHasHealth(hazardsInPlay[i].gameObject))
            {
                StartCoroutine(DestroyHazardCoroutine(hazardsInPlay[i]));
                HazardDropLoot(hazardsInPlay[i]);
                RemoveHazardFromPlay(hazardsInPlay[i]);
            }
        }

        // Movement and Collision collections
        List<GridBlock> allPossibleBlockCollisions = new List<GridBlock>();
        Vector2Int[] allOriginGridLocations = new Vector2Int[hazardsInPlay.Count];
        Vector2Int[] allDestinationGridLocations = new Vector2Int[hazardsInPlay.Count];

        // Manage Movement Data
        for (int i = hazardsInPlay.Count - 1; i > -1; i--)
        {
            GameObject hazardObject = hazardsInPlay[i].gameObject;
            MovePattern move = hazardObject.GetComponent<MovePattern>();
            move.OnTickUpdate();

            Vector2Int originGridLocation = gm.WorldToGrid(hazardsInPlay[i].currentWorldLocation);
            allOriginGridLocations[i] = originGridLocation;
            
            if (move.CanMoveThisTurn())
            {
                Debug.Log(hazardsInPlay[i].HazardName + " is moving by " + move.delta);

                Vector2Int destinationGridLocation = originGridLocation + move.delta;
                allDestinationGridLocations[i] = destinationGridLocation;

                bool moveInBounds = gm.CheckIfGridBlockInBounds(destinationGridLocation);

                if (!moveInBounds)
                {
                    StartCoroutine(DestroyHazardCoroutine(hazardsInPlay[i], 2.0f));
                    RemoveHazardFromPlay(hazardsInPlay[i]);
                    hazardDestroyedThisTick = true;
                }
                else
                {
                    gm.RemoveObjectFromGrid(hazardObject, originGridLocation);
                    Debug.Log("Removing " + hazardsInPlay[i].HazardName + " from " + originGridLocation.ToString());

                    gm.AddObjectToGrid(hazardObject, destinationGridLocation);
                    Debug.Log("Adding " + hazardsInPlay[i].HazardName + " to " + destinationGridLocation.ToString());

                    // Handle spawning cases
                    hazardObject.GetComponent<Health>().ToggleInvincibility(false);
                    hazardsInPlay[i].SetHazardAnimationMode(Hazard.HazardMode.Play);

                    hazardsInPlay[i].GetComponent<Rotator>().enabled = true;

                    hazardsInPlay[i].targetWorldLocation = gm.GridToWorld(destinationGridLocation);

                    allPossibleBlockCollisions.Add(gm.FindGridBlockByLocation(destinationGridLocation));
                }
            }
            else
            {
                hazardsInPlay[i].targetWorldLocation = hazardsInPlay[i].currentWorldLocation;
                allOriginGridLocations[i] = originGridLocation;
                allDestinationGridLocations[i] = originGridLocation;
            }
        }

        // Fly-By detection
        Debug.Log("Fly-By detection starting.");
        for (int i = 0; i < allOriginGridLocations.Length; i++)
        {
            for (int j = 0; j < allDestinationGridLocations.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }
                
                if (allOriginGridLocations[i] == allDestinationGridLocations[j] && allOriginGridLocations[j] == allDestinationGridLocations[i])
                {
                    // HazardsInPlay[i] and HazardsInPlay[j] are the Fly-By colliders
                    //Debug.LogFormat("Fly-By Object 1: {0}, Fly-By Object 2: {1}", hazardsInPlay[i], hazardsInPlay[j]);
                    
                    Hazard flyByHazard1 = hazardsInPlay[i];
                    Hazard flyByHazard2 = hazardsInPlay[j];

                    Health flyByHazard1HP = flyByHazard1.gameObject.GetComponent<Health>();
                    flyByHazard1HP.SubtractHealth(flyByHazard2.GetComponent<ContactDamage>().DamageAmount);
                    if (!CheckHazardHasHealth(flyByHazard1.gameObject))
                    {
                        // If flyByHazard1 did not survive ...
                        StartCoroutine(MoveHazardCoroutine(flyByHazard1, 0.5f));
                    }

                    Health flyByHazard2HP = flyByHazard2.gameObject.GetComponent<Health>();
                    flyByHazard2HP.SubtractHealth(flyByHazard1.GetComponent<ContactDamage>().DamageAmount);
                    if (!CheckHazardHasHealth(flyByHazard2.gameObject))
                    {
                        // If flyByHazard2 did not survive...
                        StartCoroutine(MoveHazardCoroutine(flyByHazard2, 0.5f));
                    }
                }
            }   
        }
        
        // Hazard Health check
        for (int i = hazardsInPlay.Count - 1; i > -1; i--)
        {
            GameObject hazardObject = hazardsInPlay[i].gameObject;

            if (!CheckHazardHasHealth(hazardObject))
            {
                StartCoroutine(DestroyHazardCoroutine(hazardsInPlay[i], 2.0f));
                HazardDropLoot(hazardsInPlay[i]);
                RemoveHazardFromPlay(hazardsInPlay[i]);
                hazardDestroyedThisTick = true;
            }
        }

        // Move hazards
        for (int i = hazardsInPlay.Count - 1; i > -1; i--)
        {
            if (hazardsInPlay[i].currentWorldLocation != hazardsInPlay[i].targetWorldLocation)
            {
                StartCoroutine(MoveHazardCoroutine(hazardsInPlay[i]));
                moveOccurredThisTick = true;
            }
        }

        // GridBlock Collisions
        List<GridBlock> uniquePossibleBlockCollisions = allPossibleBlockCollisions.Distinct().ToList();
        foreach (GridBlock gridBlock in uniquePossibleBlockCollisions)
        {
            if (gridBlock.objectsOnBlock.Count > 1)
            {
                Debug.Log("Processing collision on " + gridBlock.location.ToString());

                for (int i = 0; i < gridBlock.objectsOnBlock.Count; i++)
                {
                    GameObject gameObject = gridBlock.objectsOnBlock[i];
                    Health gameObjectHealth = gameObject.GetComponent<Health>();
                    ContactDamage gameObjectDamage = gameObject.GetComponent<ContactDamage>();

                    for (int j = 1 + i; j < gridBlock.objectsOnBlock.Count; j++)
                    {
                        GameObject otherGameObject = gridBlock.objectsOnBlock[j];
                        Health otherGameObjectHealth = otherGameObject.GetComponent<Health>();
                        ContactDamage otherGameObjectDamage = otherGameObject.GetComponent<ContactDamage>();

                        if (gameObjectDamage != null && otherGameObjectHealth != null)
                        {
                            Debug.Log("Subtracting " + gameObjectDamage.DamageAmount.ToString() + " from " + otherGameObject.name);
                            otherGameObjectHealth.SubtractHealth(gameObjectDamage.DamageAmount);
                        }

                        if (gameObjectHealth != null && otherGameObjectDamage != null)
                        {
                            Debug.Log("Subtracting " + otherGameObjectDamage.DamageAmount.ToString() + " from " + gameObject.name);
                            gameObjectHealth.SubtractHealth(otherGameObjectDamage.DamageAmount);
                        }
                    }
                }
            }
        }

        // Hazard Health Check
        for (int i = hazardsInPlay.Count - 1; i > -1; i--)
        {
            GameObject hazardObject = hazardsInPlay[i].gameObject;
            
            if (!CheckHazardHasHealth(hazardObject))
            {
                StartCoroutine(DestroyHazardCoroutine(hazardsInPlay[i], 2.0f));
                RemoveHazardFromPlay(hazardsInPlay[i]);
                hazardDestroyedThisTick = true;
            }
        }

        // Spawn stuff
        if (ticksUntilNewSpawn == 0 || hazardsInPlay.Count == 0)
        {
            gm.ResetSpawns();
            UpdateSpawnLocations();
            CreateHazard();
            ticksUntilNewSpawn = Random.Range(minTicksUntilSpawn, maxTicksUntilSpawn);
        }

        currentTick++;
        ticksUntilNewSpawn--;

        if (moveOccurredThisTick) delayTime += moveDurationSeconds;
        if (hazardDestroyedThisTick) delayTime += destroyDurationSeconds;
        return delayTime;
    }



    private IEnumerator MoveHazardCoroutine(Hazard hazardToMove, float hazardTravelLength = 1.0f)
    {
        float startTime = Time.time;
        float percentTraveled = 0.0f;

        while (percentTraveled <= hazardTravelLength)
        {
            float traveled = (Time.time - startTime) * 1.0f;
            percentTraveled = traveled / hazardToMove.Distance;
            hazardToMove.transform.position = Vector3.Lerp(hazardToMove.currentWorldLocation, hazardToMove.targetWorldLocation, Mathf.SmoothStep(0, 1, percentTraveled));

            yield return null;
        }

        hazardToMove.currentWorldLocation = hazardToMove.targetWorldLocation;
    }
        
    private IEnumerator DestroyHazardCoroutine(Hazard hazardToDestroy, float delay = 0.0f)
    {
        Debug.Log("DestroyHazardCoroutine() called.");
        yield return new WaitForSeconds(delay);
        Destroy(hazardToDestroy.gameObject);
        Debug.Log("DestroyHazardCoroutine() ended.");
        
        //TODO: Spawn explosion here
    }
}
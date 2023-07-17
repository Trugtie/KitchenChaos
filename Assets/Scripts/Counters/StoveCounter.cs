using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private State state;

    [SerializeField] private FryringRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private FryringRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private float fryingTime;
    private float burningTime;

    private void Start()
    {
        this.state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    this.fryingTime += Time.deltaTime;
                    if (this.fryingTime > this.fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(this.fryingRecipeSO.output, this);

                        this.state = State.Fried;
                        this.burningTime = 0f;
                        this.burningRecipeSO = GetBurningRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = this.state
                        });
                    }

                    break;
                case State.Fried:
                    this.burningTime += Time.deltaTime;
                    if (this.burningTime > this.burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(this.burningRecipeSO.output, this);

                        this.state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = this.state
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchenObject here
            if (player.HasKitchenObject())
            {
                //Player carrying somthing
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player is carrying something can fry
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    this.fryingRecipeSO = GetFryingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());


                    this.state = State.Frying;
                    this.fryingTime = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = this.state
                    });
                }
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
            //There is a kitchenObject here
            if (player.HasKitchenObject())
            {
                //Player carrying somthing
            }
            else
            {
                //Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                this.state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = this.state
                });
            }
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryringRecipeSO fryringRecipeSO = GetFryingRecipeSOFromInput(inputKitchenObjectSO);
        return fryringRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryringRecipeSO fryringRecipeSO = GetFryingRecipeSOFromInput(inputKitchenObjectSO);
        if (fryringRecipeSO != null)
        {
            return fryringRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryringRecipeSO GetFryingRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryringRecipeSO fryringRecipeSO in fryingRecipeSOArray)
        {
            if (fryringRecipeSO.input == inputKitchenObjectSO)
            {
                return fryringRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}

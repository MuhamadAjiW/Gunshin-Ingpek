using System;

[Serializable]
public class HealingPetAIController : PetAIController<HealingPet>
{
    public override void Action()
    {
        GoToward(pet.Owner.CompanionController.transform);
    }
}